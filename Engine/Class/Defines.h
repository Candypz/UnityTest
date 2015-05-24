#ifndef _DEFINES_H_
#define _DEFINES_H_
#include <windows.h>

#define DEFINES_INVALID -1
#define DEFINES_OK       1
#define DEFINES_FAIL     0
#define WinHWND          HWND

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