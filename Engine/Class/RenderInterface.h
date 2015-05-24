#ifndef _RENDERINTERFACE_H_
#define _RENDERINTERFACE_H_
#include "Defines.h"



//��Ⱦ�ӿ�

class RenderInterface {
public:
    RenderInterface() :m_screenWidth(0), m_screenHeight(0),
        m_near(0), m_far(0) {
    }
    virtual ~RenderInterface() {
    }

    virtual bool initialize(int w, int h, WinHWND hWnd,bool full) = 0;
    virtual void oneTimeInit() = 0;
    virtual void setClearColor(float r, float g, float b) = 0;//����

    //��ʼ��Ⱦ
    virtual void startRender(bool bColor, bool bDepth, bool bStencil) = 0;
    //������Ⱦ
    virtual void endRender() = 0;
    //�ر�
    virtual void shutDown() = 0;
    //���������Ⱦ�ĳ���
    virtual void clearBuffers(bool bColor,bool bDepth,bool bStencil) = 0;
    //������̬����
    virtual int createStaticBuffer(VertexType, PrimType, int totalVerts, int totalIndices,
        int stride, void **data, unsigned int *indices, int *staticId) = 0;
    //��Ⱦ
    virtual int endRendering(int staticId) = 0;
    //���ò���
    virtual void setMaterial(stMaterial *material) = 0;
    //���ù�Դ
    virtual void setLight(stLight *light, int index) = 0;
    //�رչ�Դ
    virtual void disableLight(int index) = 0;

    //͸�Ӿ���
    virtual void calculateProjMatrix(float fov, float n, float far) = 0;
    //��������
    virtual void calculateOrthoMatrix(float n, float f) = 0;

    //����͸���� ��Ⱦ״̬��ԭ��Ŀ��
    virtual void setTranspency(RenderState state,TransState src,TransState dst) = 0;
    //������� �ļ���������ָ��
    virtual int addTexture2D(char *file, int *texId) = 0;
    //���ù����� ������������������������
    virtual void setTextureFilter(int index, int filter, int val) = 0;
    //���ö�������
    virtual void setMultiTexture() = 0;
    //Ӧ������ ������ָ��������
    virtual void applyTexture(int index, int texId) = 0;
    //�������
    virtual void saveScreenShot(char *file) = 0;
    //����㾫��
    virtual void enablePointSprites(float size, float min, float a, float b, float c) = 0;
    //ͣ�õ㾫��
    virtual void disablePointSprites() = 0;

protected:
    int m_screenWidth;
    int m_screenHeight;
    bool m_fullscreen;
    WinHWND m_mainHandle;
    float m_near;//���ü���
    float m_far;//Զ�ü���
};


#endif