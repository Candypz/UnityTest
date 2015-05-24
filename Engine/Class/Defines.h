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

#endif