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
    //设置材质
    virtual void setMaterial(stMaterial *material) = 0;
    //设置光源
    virtual void setLight(stLight *light, int index) = 0;
    //关闭光源
    virtual void disableLight(int index) = 0;

    //透视矩阵
    virtual void calculateProjMatrix(float fov, float n, float far) = 0;
    //正交矩阵
    virtual void calculateOrthoMatrix(float n, float f) = 0;

    //设置透明度 渲染状态，原，目标
    virtual void setTranspency(RenderState state,TransState src,TransState dst) = 0;
    //添加纹理 文件名，纹理指针
    virtual int addTexture2D(char *file, int *texId) = 0;
    //设置过滤器 索引，多虑器，过滤器类型
    virtual void setTextureFilter(int index, int filter, int val) = 0;
    //设置多重纹理
    virtual void setMultiTexture() = 0;
    //应用纹理 索引，指定的纹理
    virtual void applyTexture(int index, int texId) = 0;
    //保存截屏
    virtual void saveScreenShot(char *file) = 0;
    //激活点精灵
    virtual void enablePointSprites(float size, float min, float a, float b, float c) = 0;
    //停用点精灵
    virtual void disablePointSprites() = 0;

protected:
    int m_screenWidth;
    int m_screenHeight;
    bool m_fullscreen;
    WinHWND m_mainHandle;
    float m_near;//近裁剪面
    float m_far;//远裁剪面
};


#endif