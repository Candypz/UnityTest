#ifndef D3DGUI_H_
#define D3DGUI_H_

#include <d3d9.h>
#include <d3dx9.h>

#define GUI_STATICTEXT 1//��̬�ı�
#define GUI_BUTTON     2//��ť
#define GUI_BACKDROP   3//����

#define GUI_BUTTON_UP   1
#define GUI_BUTTON_OVER 2
#define GUI_BUTTON_DOWN 3

#define D3DFVF_GUI (D3DFVF_XYZRHW|D3DFVF_DIFFUSE|D3DFVF_TEX1)

struct stGUIVerter {
    float x, y, z, rhw;
    unsigned long color;
    float u, v;
};

//�ؼ�����
struct stGUIControl {
    int m_type;
    int m_id;
    unsigned long m_color;

    int m_listID;
    float m_x, m_y;
    float m_width, m_height;
    char *m_text;

    LPDIRECT3DTEXTURE9 m_backDrop;
    LPDIRECT3DTEXTURE9 m_buttonUP,m_buttonDown,m_buttonOver;
};

class D3DGUISystem {
public:
    D3DGUISystem(LPDIRECT3DDEVICE9 device,int w,int h);
    ~D3DGUISystem() { shutDown(); }

    bool creatFont(char *fontName, int size, int *fontID);
    bool addBackDrop(char *fileName);
    bool addStaticText(int id, char *text, float x,float y,unsigned long color,int fontID);
    bool addButton(int id, float x, float y, char *up, char *over, char *down);
    void shutDown();

    LPD3DXFONT getFont(int id);
    stGUIControl *getGUIControl(int id);
    LPDIRECT3DVERTEXBUFFER9 getVertexBuffer(int id);
    int getTotalFonts();
    int getTotalControls();
    int getTotalBuffers();
    int getWindowWidth();
    int getWindowHeight();
    LPDIRECT3DDEVICE9 getDivice();
    stGUIControl *getBackDrop();
    LPDIRECT3DVERTEXBUFFER9 getBackDropBuffer();
    bool useBackDrop();

    void setWindowSize(int w, int h);
private:
    LPDIRECT3DDEVICE9 m_device;
    LPD3DXFONT *m_fonts;
    stGUIControl *m_guiControls;//�ؼ��б�
    LPDIRECT3DVERTEXBUFFER9 *m_vertexBuffers;//���㻺���б�
    stGUIControl m_backDrop;//����
    LPDIRECT3DVERTEXBUFFER9 m_backDropBuffer;//�������㻺��
    bool m_useBackDrop;//�Ƿ�ʹ�ñ���
    int m_totalFonts;//��������
    int m_totalCotrols;//�ؼ�����
    int m_totalBuffers;//���㻺������

    int m_windowWidth;
    int m_windowHeight;
};

//�ص�����
void processGUI(D3DGUISystem *gui, bool LMDown, int mouseX, int mouseY, void(*funcPtr)(int id, int state));


#endif