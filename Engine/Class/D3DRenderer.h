#ifndef _D3DRENDERER_H_
#define _D3DRENDERER_H_
#include "RenderInterface.h"
#include "Defines.h"
#include <d3d9.h>
#include <d3dx9.h>

#pragma comment(lib,"d3d9.lib")
#pragma comment(lib,"d3dx9.lib")

struct stD3DStaticBuffer {
    stD3DStaticBuffer() :vbPtr(0), ibPtr(0), nubVerts(0), numIndices(0), stride(0), fvf(0), primType(NULL_TYPE) {}

    LPDIRECT3DVERTEXBUFFER9 vbPtr;//����
    LPDIRECT3DINDEXBUFFER9 ibPtr;//��������
    int nubVerts;
    int numIndices;
    int stride;
    unsigned long fvf;
    PrimType primType;//���ƻ�������

};

//d3d��Ⱦ��

class D3DRenderer :public RenderInterface {
public:
    D3DRenderer();
    ~D3DRenderer();

    bool initialize(int w, int h, WinHWND hWnd, bool full);
    //͸�Ӿ���
    void calculateProjMatrix(float fov, float n, float far);
    //��������
    void calculateOrthoMatrix(float n, float f);
    void setClearColor(float r, float g, float b);

    void startRender(bool bColor, bool bDepth, bool bStencil);
    void endRender();

    void shutDown();

    int endRendering(int staticId);

    void clearBuffers(bool bColor, bool bDepth, bool bStencil);

    int createStaticBuffer(VertexType vType, PrimType primType, int totalVerts, int totalIndices, int stride, void **data, unsigned int *indices, int *staticId);

    void setMaterial(stMaterial *material);

    void setLight(stLight *light, int index);

    void disableLight(int index);
private:
    void oneTimeInit();

private:
    D3DCOLOR m_color;
    LPDIRECT3D9 m_direct3D;
    LPDIRECT3DDEVICE9 m_device;
    bool m_renderingScene;

    stD3DStaticBuffer *m_staticBufferList;//��̬����
    int m_numStaticBuffers;//��̬�����С
    int m_activeStaticBuffer;//��ǰ����ʹ�õ�
};

bool createD3DRenderer(RenderInterface **pRender);


#endif