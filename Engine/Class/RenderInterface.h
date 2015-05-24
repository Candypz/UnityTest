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

protected:
    int m_screenWidth;
    int m_screenHeight;
    bool m_fullscreen;
    WinHWND m_mainHandle;
    float m_near;//���ü���
    float m_far;//Զ�ü���
};


#endif