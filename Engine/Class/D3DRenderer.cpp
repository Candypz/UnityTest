#include "D3DRenderer.h"

bool createD3DRenderer(RenderInterface **pRender) {
    if (!*pRender) {
        *pRender = new D3DRenderer();
    }
    else {
        return false;
    }
    return true;
}

//根据顶点类型创建不通的fvf
unsigned long createD3DFvf(int flags) {
    unsigned long fvf = 0;
    return fvf;
}

D3DRenderer::D3DRenderer() {
    m_direct3D = NULL;
    m_device = NULL;
    m_renderingScene = false;
    m_numStaticBuffers = 0;
    m_activeStaticBuffer = DEFINES_INVALID;
    m_staticBufferList = NULL;
}

D3DRenderer::~D3DRenderer() {
    shutDown();
}

bool D3DRenderer::initialize(int w, int h, WinHWND hWnd, bool full) {
    //重新开始时关闭
    shutDown();

    m_mainHandle = hWnd;
    if (!m_mainHandle) {
        return false;
    }
    m_fullscreen = full;

    D3DDISPLAYMODE mode;//显示模式
    D3DCAPS9 caps;//查询显卡功能
    D3DPRESENT_PARAMETERS params;//显示参数

    ZeroMemory(&params, sizeof(params));

    m_direct3D = Direct3DCreate9(D3D_SDK_VERSION);//创建d3d对象
    if (!m_direct3D) {
        return false;
    }

    if (FAILED(m_direct3D->GetAdapterDisplayMode(D3DADAPTER_DEFAULT, &mode))) {
        return false;
    }

    if (FAILED(m_direct3D->GetDeviceCaps(D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, &caps))) {
        return false;
    }

    DWORD processing = 0;
    //是否有硬件处理能力
    if (caps.VertexProcessingCaps != 0) {
        processing = D3DCREATE_HARDWARE_VERTEXPROCESSING | D3DCREATE_PUREDEVICE;
    }
    else {
        processing = D3DCREATE_SOFTWARE_VERTEXPROCESSING;
    }

    //全屏
    if (m_fullscreen) {
        params.FullScreen_RefreshRateInHz = mode.RefreshRate;
        //显示速率
        params.PresentationInterval = D3DPRESENT_INTERVAL_ONE;
    }
    else {
        //立即显示
        params.PresentationInterval = D3DPRESENT_INTERVAL_IMMEDIATE;
    }

    params.Windowed = !m_fullscreen;
    params.BackBufferWidth = w;
    params.BackBufferHeight = h;
    params.hDeviceWindow = m_mainHandle;//游戏窗口
    params.SwapEffect = D3DSWAPEFFECT_DISCARD;//交换
    params.BackBufferFormat = mode.Format;//显示模式
    params.BackBufferCount = 1;
    params.EnableAutoDepthStencil = TRUE;
    params.AutoDepthStencilFormat = D3DFMT_D16;//格式

    m_screenWidth = w;
    m_screenHeight = h;

    if (FAILED(m_direct3D->CreateDevice(D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, m_mainHandle, processing, &params, &m_device))) {
        return false;
    }

    if (m_device == NULL) {
        return false;
    }

    oneTimeInit();

    return true;
}

void D3DRenderer::oneTimeInit() {
    if (!m_device) {
        return;
    }
    //渲染状态
    m_device->SetRenderState(D3DRS_LIGHTING, false);
    m_device->SetRenderState(D3DRS_CULLMODE, D3DCULL_NONE);

    calculateProjMatrix(D3DX_PI / 4, 0.1f, 1000);
}

void D3DRenderer::calculateProjMatrix(float fov, float n, float f) {
    if (!m_device) {
        return;
    }
    D3DXMATRIX projection;

    D3DXMatrixPerspectiveFovLH(&projection, fov, (float)(m_screenWidth / m_screenHeight), n, f);
    m_device->SetTransform(D3DTS_PROJECTION, &projection);
}

void D3DRenderer::calculateOrthoMatrix(float n, float f) {
    if (!m_device) {
        return;
    }
    D3DXMATRIX ortho;
    D3DXMatrixOrthoLH(&ortho, (float)m_screenWidth, (float)m_screenHeight, n, f);
    m_device->SetTransform(D3DTS_PROJECTION, &ortho);
}

void D3DRenderer::setClearColor(float r, float g, float b) {
    m_color = D3DCOLOR_COLORVALUE(r, g, b, 1.0f);
}

void D3DRenderer::startRender(bool bColor, bool bDepth, bool bStencil) {
    if (!m_device) {
        return;
    }

    //决定是否清除传入的是否
    unsigned int buffers = 0;
    if (bColor) {
        buffers |= D3DCLEAR_TARGET;
    }
    if (bDepth) {
        buffers |= D3DCLEAR_ZBUFFER;
    }
    if (bStencil) {
        buffers |= D3DCLEAR_STENCIL;
    }

    if (FAILED(m_device->Clear(0, NULL, buffers, m_color, 1, 0))) {
        return;
    }
    if (FAILED(m_device->BeginScene())) {
        return;
    }
    m_renderingScene = true;
}

void D3DRenderer::endRender() {
    if (!m_device) {
        return;
    }

    m_device->EndScene();
    //显示场景
    m_device->Present(NULL, NULL, NULL, NULL);
    m_renderingScene = false;
}

void D3DRenderer::clearBuffers(bool bColor, bool bDepth, bool bStencil) {
    if (!m_device) {
        return;
    }

    //决定是否清除传入的是否
    unsigned int buffers = 0;
    if (bColor) {
        buffers |= D3DCLEAR_TARGET;
    }
    if (bDepth) {
        buffers |= D3DCLEAR_ZBUFFER;
    }
    if (bStencil) {
        buffers |= D3DCLEAR_STENCIL;
    }
    if (m_renderingScene) {
        m_device->EndScene();
    }

    if (FAILED(m_device->Clear(0, NULL, buffers, m_color, 1.0f, 0))) {
        return;
    }

    if (m_renderingScene) {
        if (FAILED(m_device->BeginScene())) {
            return;
        }
    }
}

int D3DRenderer::createStaticBuffer(VertexType vType, PrimType primType, int totalVerts, int totalIndices, int stride, void **data, unsigned int *indices, int *staticId) {
    int index = m_numStaticBuffers;
    void **ptr;

    if (!m_staticBufferList) {
        m_staticBufferList = new stD3DStaticBuffer[1];
        if (!m_staticBufferList) {
            return DEFINES_FAIL;
        }
    }
    else {
        //动态数组
        stD3DStaticBuffer *temp;
        temp = new stD3DStaticBuffer[m_numStaticBuffers + 1];

        memcpy(temp, m_staticBufferList, sizeof(stD3DStaticBuffer)*m_numStaticBuffers);
        delete[] m_staticBufferList;
        m_staticBufferList = temp;
    }
    m_staticBufferList[index].nubVerts = totalVerts;
    m_staticBufferList[index].numIndices = totalIndices;
    m_staticBufferList[index].primType = primType;
    m_staticBufferList[index].stride = stride;
    m_staticBufferList[index].fvf = createD3DFvf(vType);

    if (totalIndices > 0) {
        //创建顶点缓存
        if (FAILED(m_device->CreateIndexBuffer(sizeof(unsigned int)*totalIndices, D3DUSAGE_WRITEONLY, D3DFMT_D16, D3DPOOL_DEFAULT,
            &m_staticBufferList[index].ibPtr, NULL))) {
            return DEFINES_FAIL;
        }
        if (FAILED(m_staticBufferList[index].ibPtr->Lock(0, 0, (void**)&ptr, 0))) {
            return DEFINES_FAIL;
        }
        memcpy(ptr, indices, sizeof(unsigned int)*totalIndices);
        m_staticBufferList[index].ibPtr->Unlock();
    }
    else {
        m_staticBufferList[index].ibPtr = NULL;
    }
    if (FAILED(m_device->CreateVertexBuffer(totalVerts*stride, D3DUSAGE_WRITEONLY, m_staticBufferList[index].fvf,
        D3DPOOL_DEFAULT, &m_staticBufferList[index].vbPtr, NULL))) {
        return DEFINES_FAIL;
    }

    if (m_staticBufferList[index].vbPtr->Lock(0, 0, (void**)&ptr, 0)) {
        return DEFINES_FAIL;
    }
    memcpy(ptr, data, totalVerts*stride);
    m_staticBufferList[index].vbPtr->Unlock();

    //最新的
    *staticId = m_numStaticBuffers;
    m_numStaticBuffers++;//为下一次添加图形
    return DEFINES_OK;
}

void D3DRenderer::shutDown() {
    for (int i = 0; i < m_numStaticBuffers; ++i) {
        if (m_staticBufferList[i].vbPtr) {
            m_staticBufferList[i].vbPtr->Release();
            m_staticBufferList[i].vbPtr = NULL;
        }
        if (m_staticBufferList[i].ibPtr) {
            m_staticBufferList[i].ibPtr->Release();
            m_staticBufferList[i].ibPtr = NULL;
        }
    }
    m_numStaticBuffers = 0;
    if (m_staticBufferList) {
        delete[] m_staticBufferList;
        m_staticBufferList = NULL;
    }
    if (m_device) {
        m_device->Release();
        m_device = NULL;
    }
    if (m_direct3D) {
        m_direct3D->Release();
        m_direct3D = NULL;
    }
}

int D3DRenderer::endRendering(int staticId) {
    /*m_device->Clear();
    m_device->BeginScene();

    m_device->SetIndices();
    m_device->SetStreamSource();
    m_device->SetFVF();
    m_device->DrawIndexedPrimitive();
    m_device->DrawPrimitive();


    m_device->EndScene();
    m_device->Present();
    */
    if (staticId >= m_numStaticBuffers) {
        return DEFINES_FAIL;
    }
    if (m_activeStaticBuffer != staticId) {
        if (m_staticBufferList[staticId].ibPtr != NULL) {
            m_device->SetIndices(m_staticBufferList[staticId].ibPtr);
        }

        m_device->SetStreamSource(0, m_staticBufferList[staticId].vbPtr, 0, m_staticBufferList[staticId].stride);
        m_device->SetFVF(m_staticBufferList[staticId].fvf);

        m_activeStaticBuffer = staticId;
    }
    if (m_staticBufferList[staticId].ibPtr != NULL) {
        switch (m_staticBufferList[staticId].primType) {
        case POINT_LIST:
            if (FAILED(m_device->DrawPrimitive(D3DPT_POINTLIST, 0, m_staticBufferList[staticId].nubVerts))) {
                return DEFINES_FAIL;
            }
            break;
        case  TRIANGLE_LIST:
            if (FAILED(m_device->DrawIndexedPrimitive(D3DPT_TRIANGLELIST, 0, 0, m_staticBufferList[staticId].nubVerts / 3, 0, m_staticBufferList[staticId].numIndices))) {
                return DEFINES_FAIL;
            }
            break;
        case  TRIANGLE_STRIP:
            if (FAILED(m_device->DrawIndexedPrimitive(D3DPT_TRIANGLESTRIP, 0, 0, m_staticBufferList[staticId].nubVerts / 2, 0, m_staticBufferList[staticId].numIndices))) {
                return DEFINES_FAIL;
            }
            break;
        case  TRIANGLE_FAN:
            if (FAILED(m_device->DrawIndexedPrimitive(D3DPT_TRIANGLEFAN, 0, 0, m_staticBufferList[staticId].nubVerts / 2, 0, m_staticBufferList[staticId].numIndices))) {
                return DEFINES_FAIL;
            }
            break;
        case  LINE_LIST:
            if (FAILED(m_device->DrawIndexedPrimitive(D3DPT_LINELIST, 0, 0, m_staticBufferList[staticId].nubVerts / 2, 0, m_staticBufferList[staticId].numIndices))) {
                return DEFINES_FAIL;
            }
            break;
        case  LINE_STRIP:
            if (FAILED(m_device->DrawIndexedPrimitive(D3DPT_LINESTRIP, 0, 0, m_staticBufferList[staticId].nubVerts, 0, m_staticBufferList[staticId].numIndices))) {
                return DEFINES_FAIL;
            }
            break;
        default:
            return DEFINES_FAIL;
        }
    }
    else {
        switch (m_staticBufferList[staticId].primType) {
        case POINT_LIST:
            if (FAILED(m_device->DrawPrimitive(D3DPT_POINTLIST, 0, m_staticBufferList[staticId].nubVerts))) {
                return DEFINES_FAIL;
            }
            break;
        case  TRIANGLE_LIST:
            if (FAILED(m_device->DrawPrimitive(D3DPT_TRIANGLELIST, 0, (int)(m_staticBufferList[staticId].nubVerts) / 3))) {
                return DEFINES_FAIL;
            }
            break;
        case  TRIANGLE_STRIP:
            if (FAILED(m_device->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, (int)(m_staticBufferList[staticId].nubVerts) / 2))) {
                return DEFINES_FAIL;
            }
            break;
        case  TRIANGLE_FAN:
            if (FAILED(m_device->DrawPrimitive(D3DPT_TRIANGLEFAN, 0, (int)(m_staticBufferList[staticId].nubVerts) / 2))) {
                return DEFINES_FAIL;
            }
            break;
        case  LINE_LIST:
            if (FAILED(m_device->DrawPrimitive(D3DPT_LINELIST, 0, (int)(m_staticBufferList[staticId].nubVerts) / 2))) {
                return DEFINES_FAIL;
            }
            break;
        case  LINE_STRIP:
            if (FAILED(m_device->DrawPrimitive(D3DPT_LINESTRIP, 0, (int)(m_staticBufferList[staticId].nubVerts)))) {
                return DEFINES_FAIL;
            }
            break;
        default:
            return DEFINES_FAIL;
        }
    }
    return DEFINES_OK;
}