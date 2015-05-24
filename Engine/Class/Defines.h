#ifndef _DEFINES_H_
#define _DEFINES_H_
#include <windows.h>
#include "Material.h"
#include "Light.h"

#define DEFINES_INVALID  -1
#define DEFINES_OK        1
#define DEFINES_FAIL      0
#define WinHWND           HWND

//光源类型
#define LIGHT_POINT       1
#define LIGHT_SPOT        2
#define LIGHT_DIRECTIONAL 3


//顶点类型
typedef long VertexType;

//图源类型
enum PrimType {
    NULL_TYPE,
    POINT_LIST,
    TRIANGLE_LIST,
    TRIANGLE_STRIP,
    TRIANGLE_FAN,
    LINE_LIST,
    LINE_STRIP
};

//渲染状态
enum RenderState {
    CULL_NONE,//无剪裁
    CULL_CW,//顺时针剪裁
    CULL_CCW,//逆时针剪裁
    DEPTH_NONE,//无深度缓存
    DEPTH_READONLY,//只读的深度缓存
    DEPTH_READWRITE,//读写的深度缓存
    SHADE_POINTS,//点着色
    SHADE_SOLIDTRI,//实心三角形着色
    SHADE_WIRETR,//三角形线框着色
    SHADE_WIREPOLY,//多边形线框着色
    TRANSARENCY_NONE,//无透明度
    TRANSARENCY_ENABLE,//激活透明度
};

//纹理透明度,融合因子
enum TransState {
    TRANS_ZERO = 1,
    TRANS_ONE,
    TRANS_SRCCOLOR,
    TRANS_INVSRCCOLOR,
    TRANS_SRCALPHA,
    TRANS_INVSRCALPHA,
    TRANS_DSTALPHA,
    TRANS_INVDSTCOLOR,
    TRANS_SRCALPHASAT,
    TRANS_BOTHSRCALPHA,
    TRANS_INVBOTHSRCALPHA,
    TRANS_BLENDFACTOR,
    TRANS_INVBLENDFACTOR,
};

//纹理状态
enum TextureState {
    MIN_FILTER,//缩小
    MAG_FILTER,//放大
    MIP_FILTER,//多级过滤
};

//过滤器类型
enum FilterType {
    POINT_TYPE,
    LINEAR_TYPE,
    ANISOTROPIC_TYPE//各项异性过滤器
};

#endif