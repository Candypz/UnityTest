#ifndef _RENDERINTERFACE_H_
#define _RENDERINTERFACE_H_
#include "Defines.h"


//渲染接口

class RenderInterface {
public:
    RenderInterface() :m_screenWidth(0), m_screenHeight(0),
        m_near(0), m_far(0) {
    }
    virtual ~RenderInterface() {
    }

    virtual bool initialize(int w, int h, WinHWND hWnd,bool full) = 0;
    virtual void oneTimeInit() = 0;
    virtual void setClearColor(float r, float g, float b) = 0;//清屏

    //开始渲染
    virtual void startRender(bool bColor, bool bDepth, bool bStencil) = 0;
    //结束渲染
    virtual void endRender() = 0;
    //关闭
    virtual void shutDown() = 0;
    //清除正在渲染的场景
    virtual void clearBuffers(bool bColor,bool bDepth,bool bStencil) = 0;
    //创建静态缓存
    virtual int createStaticBuffer(VertexType, PrimType, int totalVerts, int totalIndices,
        int stride, void **data, unsigned int *indices, int *staticId) = 0;
    //渲染
    virtual int endRendering(int staticId) = 0;

protected:
    int m_screenWidth;
    int m_screenHeight;
    bool m_fullscreen;
    WinHWND m_mainHandle;
    float m_near;//近裁剪面
    float m_far;//远裁剪面
};


#endif