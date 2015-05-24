#ifndef _DEFINES_H_
#define _DEFINES_H_
#include <windows.h>
#include "Material.h"
#include "Light.h"

#define DEFINES_INVALID  -1
#define DEFINES_OK        1
#define DEFINES_FAIL      0
#define WinHWND           HWND

//��Դ����
#define LIGHT_POINT       1
#define LIGHT_SPOT        2
#define LIGHT_DIRECTIONAL 3


//��������
typedef long VertexType;

//ͼԴ����
enum PrimType {
    NULL_TYPE,
    POINT_LIST,
    TRIANGLE_LIST,
    TRIANGLE_STRIP,
    TRIANGLE_FAN,
    LINE_LIST,
    LINE_STRIP
};

//��Ⱦ״̬
enum RenderState {
    CULL_NONE,//�޼���
    CULL_CW,//˳ʱ�����
    CULL_CCW,//��ʱ�����
    DEPTH_NONE,//����Ȼ���
    DEPTH_READONLY,//ֻ������Ȼ���
    DEPTH_READWRITE,//��д����Ȼ���
    SHADE_POINTS,//����ɫ
    SHADE_SOLIDTRI,//ʵ����������ɫ
    SHADE_WIRETR,//�������߿���ɫ
    SHADE_WIREPOLY,//������߿���ɫ
    TRANSARENCY_NONE,//��͸����
    TRANSARENCY_ENABLE,//����͸����
};

//����͸����,�ں�����
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

//����״̬
enum TextureState {
    MIN_FILTER,//��С
    MAG_FILTER,//�Ŵ�
    MIP_FILTER,//�༶����
};

//����������
enum FilterType {
    POINT_TYPE,
    LINEAR_TYPE,
    ANISOTROPIC_TYPE//�������Թ�����
};

#endif