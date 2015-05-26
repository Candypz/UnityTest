#include "D3DGUI.h"

D3DGUISystem::D3DGUISystem(LPDIRECT3DDEVICE9 device, int w, int h) {
    m_fonts = NULL;
    m_guiControls = NULL;
    m_vertexBuffers = NULL;

    m_totalFonts = m_totalCotrols = m_totalBuffers = 0;
    m_windowWidth = w;
    m_windowHeight = h;
    m_useBackDrop = false;

    m_device = device;

    memset(&m_backDrop, 0, sizeof(stGUIControl));
}

LPD3DXFONT D3DGUISystem::getFont(int id) {
    if (id < 0 || id >= m_totalFonts) {
        return NULL;
    }
    return m_fonts[id];
}

stGUIControl *D3DGUISystem::getGUIControl(int id) {
    if (id < 0 || id >= m_totalCotrols) {
        return NULL;
    }
    return &m_guiControls[id];
}

LPDIRECT3DVERTEXBUFFER9 D3DGUISystem::getVertexBuffer(int id) {
    if (id < 0 || id >= m_totalBuffers) {
        return NULL;
    }
    return m_vertexBuffers[id];
}

int D3DGUISystem::getTotalFonts() {
    return m_totalFonts;
}

int D3DGUISystem::getTotalControls() {
    return m_totalCotrols;
}

int D3DGUISystem::getTotalBuffers() {
    return m_totalBuffers;
}

int D3DGUISystem::getWindowWidth() {
    return m_windowWidth;
}

int D3DGUISystem::getWindowHeight() {
    return m_windowHeight;
}

LPDIRECT3DDEVICE9 D3DGUISystem::getDivice() {
    return m_device;
}

stGUIControl *D3DGUISystem::getBackDrop() {
    return &m_backDrop;
}

LPDIRECT3DVERTEXBUFFER9 D3DGUISystem::getBackDropBuffer() {
    return m_backDropBuffer;
}

bool D3DGUISystem::useBackDrop() {
    return m_useBackDrop;
}

void D3DGUISystem::setWindowSize(int w, int h) {
    m_windowWidth = w;
    m_windowHeight = h;
}

bool D3DGUISystem::addBackDrop(char *fileName) {
    if (!fileName) {
        return false;
    }

    m_backDrop.m_type = GUI_BACKDROP;

    if (D3DXCreateTextureFromFile(m_device, fileName, &m_backDrop.m_backDrop) != D3D_OK) {
        return false;
    }

    float w = (float)m_windowWidth;
    float h = (float)m_windowHeight;

    stGUIVerter obj[] =
    {
        { w, 0, 0.0f, 1, D3DCOLOR_XRGB(255, 255, 255), 1.0f, 0.0f },
        { w, h, 0.0f, 1, D3DCOLOR_XRGB(255, 255, 255), 1.0f, 1.0f },
        { 0, 0, 0.0f, 1, D3DCOLOR_XRGB(255, 255, 255), 0.0f, 0.0f },
        { 0, h, 0.0f, 1, D3DCOLOR_XRGB(255, 255, 255), 0.0f, 1.0f },
    };

    if (FAILED(m_device->CreateVertexBuffer(sizeof(obj), 0, D3DFVF_GUI,
        D3DPOOL_DEFAULT, &m_backDropBuffer, NULL))) return false;

    void *ptr;
    if (FAILED(m_backDropBuffer->Lock(0, sizeof(obj), (void**)&ptr, 0))) {
        return false;
    }
    memcpy(ptr, obj, sizeof(obj));
    m_backDropBuffer->Unlock();

    m_useBackDrop = true;

    return true;
}

bool D3DGUISystem::creatFont(char *fontName, int size, int *fontID) {
    if (!m_device) {
        return false;
    }
    if (!m_fonts) {
        m_fonts = new LPD3DXFONT[1];
        if (!m_fonts) {
            return false;
        }
    }
    else {
        LPD3DXFONT *temp;
        temp = new LPD3DXFONT[m_totalFonts + 1];
        if (!temp) {
            return false;
        }
        memcpy(temp, m_fonts, sizeof(LPD3DXFONT)*m_totalFonts);
        delete[] m_fonts;
        m_fonts = temp;
    }

    if (FAILED(D3DXCreateFont(m_device, size, 0, 0, 0, 0, 0, 0, 0, 0, fontName, &m_fonts[m_totalFonts]))) {
        return false;
    }

    if (!m_fonts[m_totalFonts]) {
        return false;
    }

    if (fontID) {
        *fontID = m_totalFonts;
    }
    m_totalFonts++;

    return true;
}

bool D3DGUISystem::addStaticText(int id, char *text, float x, float y, unsigned long color, int fontID) {
    if (!text || fontID < 0 || fontID >= m_totalFonts) {
        return false;
    }
    if (!m_guiControls) {
        m_guiControls = new stGUIControl[1];
        if (!m_guiControls) {
            return false;
        }
        memset(&m_guiControls[0], 0, sizeof(stGUIControl));
    }
    else {
        stGUIControl *temp;
        temp = new stGUIControl[m_totalCotrols + 1];
        if (!temp) {
            return false;
        }
        memset(temp, 0, sizeof(stGUIControl)*(m_totalCotrols + 1));
        memcpy(temp, m_guiControls, sizeof(stGUIControl)*m_totalCotrols);
        delete[] m_guiControls;
        m_guiControls = temp;
    }
    m_guiControls[m_totalCotrols].m_type = GUI_STATICTEXT;
    m_guiControls[m_totalCotrols].m_id = id;
    m_guiControls[m_totalCotrols].m_color = color;
    m_guiControls[m_totalCotrols].m_x = x;
    m_guiControls[m_totalCotrols].m_y = y;
    m_guiControls[m_totalCotrols].m_listID = fontID;

    int len = strlen(text);
    m_guiControls[m_totalCotrols].m_text = new char[len + 1];
    if (!m_guiControls[m_totalCotrols].m_text) {
        return false;
    }
    memcpy(m_guiControls[m_totalCotrols].m_text, text, len);
    m_guiControls[m_totalCotrols].m_text[len] = '\0';
    m_totalCotrols++;

    return true;
}

bool D3DGUISystem::addButton(int id, float x, float y, char *up, char *over, char *down) {
    if (!up || !over || !down) {
        return false;
    }
    if (!m_guiControls) {
        m_guiControls = new stGUIControl[1];
        if (!m_guiControls) {
            return false;
        }
        memset(&m_guiControls[0], 0, sizeof(stGUIControl));
    }
    else {
        stGUIControl *temp;
        temp = new stGUIControl[m_totalCotrols + 1];
        if (!temp) {
            return false;
        }
        memset(temp, 0, sizeof(stGUIControl)*(m_totalCotrols + 1));
        memcpy(temp, m_guiControls, sizeof(stGUIControl)*m_totalCotrols);
        delete[]m_guiControls;
        m_guiControls = temp;
    }
    m_guiControls[m_totalCotrols].m_type = GUI_BUTTON;
    m_guiControls[m_totalCotrols].m_id = id;
    m_guiControls[m_totalCotrols].m_x = x;
    m_guiControls[m_totalCotrols].m_y = y;
    m_guiControls[m_totalCotrols].m_listID = m_totalBuffers;//顶点缓存列表里的

    if (D3DXCreateTextureFromFile(m_device, up, &m_guiControls[m_totalCotrols].m_buttonUP) != D3D_OK) {
        return false;
    }
    if (D3DXCreateTextureFromFile(m_device, over, &m_guiControls[m_totalCotrols].m_buttonOver) != D3D_OK) {
        return false;
    }
    if (D3DXCreateTextureFromFile(m_device, down, &m_guiControls[m_totalCotrols].m_buttonDown) != D3D_OK) {
        return false;
    }

    unsigned long white = D3DCOLOR_XRGB(255, 255, 255);

    D3DSURFACE_DESC desc;//获取图像大小
    m_guiControls[m_totalCotrols].m_buttonUP->GetLevelDesc(0, &desc);
    float w = (float)desc.Width;
    float h = (float)desc.Height;
    m_guiControls[m_totalCotrols].m_width = w;
    m_guiControls[m_totalCotrols].m_height = h;

    stGUIVerter obj[] = {
        { w + x, 0 + y, 0.0f, 1, white, 1.0f, 0.0f },
        { w + x, h + y, 0.0f, 1, white, 1.0f, 1.0f },
        { 0 + x, 0 + y, 0.0f, 1, white, 0.0f, 0.0f },
        { 0 + x, h + y, 0.0f, 1, white, 0.0f, 1.0f },
    };

    if (!m_vertexBuffers) {
        m_vertexBuffers = new LPDIRECT3DVERTEXBUFFER9[1];
        if (!m_vertexBuffers) {
            return false;
        }
    }
    else {
        LPDIRECT3DVERTEXBUFFER9 *temp;
        temp = new LPDIRECT3DVERTEXBUFFER9[m_totalBuffers + 1];
        if (!temp) {
            return false;
        }
        memcpy(temp, m_vertexBuffers, sizeof(LPDIRECT3DVERTEXBUFFER9)*m_totalBuffers);
        delete[]m_vertexBuffers;
        m_vertexBuffers = temp;
    }
    if (FAILED(m_device->CreateVertexBuffer(sizeof(obj), 0, D3DFVF_GUI, D3DPOOL_DEFAULT, &m_vertexBuffers[m_totalBuffers], NULL))) {
        return false;
    }

    void **ptr;
    if (FAILED(m_vertexBuffers[m_totalBuffers]->Lock(0, sizeof(obj), (void**)&ptr, 0))) {
        return false;
    }
    memcpy(ptr, obj, sizeof(obj));
    m_vertexBuffers[m_totalBuffers]->Unlock();
    m_totalBuffers++;
    m_totalCotrols++;

    return true;
}

void D3DGUISystem::shutDown() {
    if (m_useBackDrop) {
        if (m_backDrop.m_backDrop) {
            m_backDrop.m_backDrop->Release();
            m_backDrop.m_backDrop = NULL;
        }
        if (m_backDropBuffer) {
            m_backDropBuffer->Release();
            m_backDropBuffer = NULL;
        }
    }
    for (int i = 0; i < m_totalFonts; ++i) {
        if (m_fonts[i]) {
            m_fonts[i]->Release();
            m_fonts[i] = NULL;
        }
    }
    if (m_fonts) {
        delete[] m_fonts;
        m_fonts = NULL;
    }
    m_totalFonts = 0;

    for (int i = 0; i < m_totalBuffers; ++i) {
        if (m_vertexBuffers[i]) {
            m_vertexBuffers[i]->Release();
            m_vertexBuffers[i] = NULL;
        }
    }
    if (m_vertexBuffers) {
        delete[] m_vertexBuffers;
        m_vertexBuffers = NULL;
    }
    m_totalBuffers = 0;

    for (int i = 0; i < m_totalCotrols; ++i) {
        if (m_guiControls[i].m_backDrop) {
            m_guiControls[i].m_backDrop->Release();
            m_guiControls[i].m_backDrop = NULL;
        }
        if (m_guiControls[i].m_buttonUP) {
            m_guiControls[i].m_buttonUP->Release();
            m_guiControls[i].m_buttonUP = NULL;
        }
        if (m_guiControls[i].m_buttonOver) {
            m_guiControls[i].m_buttonOver->Release();
            m_guiControls[i].m_buttonOver = NULL;
        }
        if (m_guiControls[i].m_buttonDown) {
            m_guiControls[i].m_buttonDown->Release();
            m_guiControls[i].m_buttonDown = NULL;
        }
        if (m_guiControls[i].m_text) {
            delete[]m_guiControls[i].m_text;
            m_guiControls[i].m_text = NULL;
        }
    }
    if (m_guiControls) {
        delete[] m_guiControls;
        m_guiControls = NULL;
    }
    m_totalCotrols = 0;
}

void processGUI(D3DGUISystem *gui, bool LMDown, int mouseX, int mouseY, void(*funcPtr)(int id, int state)) {
    if (!gui) {
        return;
    }

    LPDIRECT3DDEVICE9 device = gui->getDivice();
    if (!device) {
        return;
    }

    stGUIControl *backDrop = gui->getBackDrop();
    LPDIRECT3DVERTEXBUFFER9 bdBffer = gui->getBackDropBuffer();
    if (gui->useBackDrop() && backDrop&&bdBffer) {
        device->SetTexture(0, backDrop->m_backDrop);
        device->SetStreamSource(0, bdBffer, 0, sizeof(stGUIVerter));
        device->SetFVF(D3DFVF_GUI);
        device->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, 2);
        device->SetTexture(0, NULL);
    }

    LPD3DXFONT pFont = NULL;
    RECT fontPosition = { 0, 0, (long)gui->getWindowWidth(), (long)gui->getWindowHeight() };

    LPDIRECT3DVERTEXBUFFER9 pBuffer = NULL;
    int status = GUI_BUTTON_UP;

    for (int i = 0; i < gui->getTotalControls(); ++i) {
        stGUIControl *pCont = gui->getGUIControl(i);
        if (!pCont) continue;

        switch (pCont->m_type) {
            case GUI_STATICTEXT:
                pFont = gui->getFont(pCont->m_listID);
                if (!pFont) {
                    continue;
                }
                fontPosition.left = pCont->m_x;
                fontPosition.top = pCont->m_y;
                pFont->DrawTextA(NULL, pCont->m_text, -1, &fontPosition, DT_LEFT, pCont->m_color);
                break;
            case GUI_BUTTON:
                status = GUI_BUTTON_UP;
                pBuffer = gui->getVertexBuffer(pCont->m_listID);
                if (!pBuffer) {
                    continue;
                }
                device->SetRenderState(D3DRS_ALPHABLENDENABLE, true);
                device->SetRenderState(D3DRS_SRCBLEND, D3DBLEND_SRCALPHA);
                device->SetRenderState(D3DRS_DESTBLEND, D3DBLEND_INVDESTALPHA);

                if (mouseX > pCont->m_x&&mouseX<pCont->m_x + pCont->m_width&&mouseY>pCont->m_y&&mouseY < pCont->m_y + pCont->m_height) {
                    if (LMDown) {
                        status = GUI_BUTTON_DOWN;
                    }
                    else {
                        status = GUI_BUTTON_OVER;
                    }
                }
                if (status == GUI_BUTTON_UP) {
                    device->SetTexture(0, pCont->m_buttonUP);
                }
                if (status == GUI_BUTTON_OVER) {
                    device->SetTexture(0, pCont->m_buttonOver);
                }
                if (status == GUI_BUTTON_DOWN) {
                    device->SetTexture(0, pCont->m_buttonDown);
                }

                device->SetStreamSource(0, pBuffer, 0, sizeof(stGUIVerter));
                device->SetFVF(D3DFVF_GUI);
                device->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, 2);

                device->SetRenderState(D3DRS_ALPHABLENDENABLE, false);
                break;
        }
        if (funcPtr) {//调用回掉函数
            funcPtr(pCont->m_id, status);
        }
    }
}