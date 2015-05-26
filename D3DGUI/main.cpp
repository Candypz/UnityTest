
#include "D3DGUI.h"

#pragma comment(lib, "d3d9.lib")
#pragma comment(lib, "d3dx9.lib")

#define WINDOW_CLASS	"UGPDX"
#define WINDOW_TITLE	"Demo Window"
#define WINDOW_WIDTH	640
#define WINDOW_HEIGHT	480

#define STATIC_ID_1     1
#define STATIC_ID_2     2

#define BUTTON_ID_1     3
#define BUTTON_ID_2     4
#define BUTTON_ID_3     5
#define BUTTON_ID_4     6

bool InitializeD3D(HWND hWnd, bool fullscreen);
bool InitializeObjects();
void RenderScene();
void Shutdown();

LPDIRECT3D9 g_D3D = NULL;
LPDIRECT3DDEVICE9 g_D3DDevice = NULL;

D3DGUISystem *g_gui = NULL;

D3DXMATRIX g_projection;
D3DXMATRIX g_ViewMatrix;
D3DXMATRIX g_WorldMatrix;

bool LMBDown = false;
int mouseX = 0;
int mouseY = 0;

int arialID = -1;
int timeID = -1;



LRESULT WINAPI MsgProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch(msg)
	{
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
		break;
	case WM_KEYUP:
		if(wParam == VK_ESCAPE) PostQuitMessage(0);
		break;
    case WM_LBUTTONDOWN:
        LMBDown = true;
        break;
    case WM_LBUTTONUP:
        LMBDown = false;
        break;
    case WM_MOUSEMOVE:
        mouseX = LOWORD(lParam);
        mouseY = HIWORD(lParam);
        break;
	}


	return DefWindowProc(hWnd,msg,wParam,lParam);
}

int WINAPI WinMain(HINSTANCE hInst,
				HINSTANCE prevhInst,
				LPTSTR cmdLine,
				int show)
{
	WNDCLASSEX wc = { sizeof(WNDCLASSEX),CS_CLASSDC,MsgProc,0,0,
		hInst,NULL,NULL,NULL,NULL,WINDOW_CLASS,NULL };

	RegisterClassEx(&wc);

	HWND hWnd = CreateWindow(WINDOW_CLASS,WINDOW_TITLE,WS_OVERLAPPEDWINDOW,
		100,100,WINDOW_WIDTH,WINDOW_HEIGHT,GetDesktopWindow(),NULL,hInst,NULL);

	if(InitializeD3D(hWnd,false))
	{
		ShowWindow(hWnd,SW_SHOWDEFAULT);
		UpdateWindow(hWnd);

		MSG msg;
		ZeroMemory(&msg, sizeof(msg));

		while(msg.message != WM_QUIT)
		{
			if(PeekMessage(&msg,NULL,0,0,PM_REMOVE))
			{
				TranslateMessage(&msg);
				DispatchMessage(&msg);
			}
			else
			{
				RenderScene();
			}
		}
	}

	Shutdown();

	UnregisterClass(WINDOW_CLASS,hInst);

	return 0;
}

bool InitializeD3D(HWND hWnd, bool fullscreen)
{
	D3DDISPLAYMODE displayMode;

	g_D3D = Direct3DCreate9(D3D_SDK_VERSION);
	if(g_D3D == NULL) return false;

	if(FAILED(g_D3D->GetAdapterDisplayMode(D3DADAPTER_DEFAULT,&displayMode)))
	{
		return false;
	}

	D3DPRESENT_PARAMETERS d3dpp;
	ZeroMemory(&d3dpp,sizeof(d3dpp));

	if(fullscreen)
	{
		d3dpp.Windowed = FALSE;
		d3dpp.BackBufferWidth = WINDOW_WIDTH;
		d3dpp.BackBufferHeight = WINDOW_HEIGHT;
	}else
		d3dpp.Windowed = TRUE;

	d3dpp.BackBufferFormat = displayMode.Format;
	d3dpp.SwapEffect = D3DSWAPEFFECT_DISCARD;

	if(FAILED(g_D3D->CreateDevice(D3DADAPTER_DEFAULT,D3DDEVTYPE_HAL,hWnd,D3DCREATE_HARDWARE_VERTEXPROCESSING,&d3dpp,&g_D3DDevice)))
	{
		return false;
	}

	if(!InitializeObjects())
		return false;

	return true;
}

bool InitializeObjects()
{
    g_gui = new D3DGUISystem(g_D3DDevice, WINDOW_WIDTH, WINDOW_HEIGHT);

    if (!g_gui->addBackDrop("backdrop.jpg")) {
        return false;
    }
    if (!g_gui->creatFont("Arial", 18, &arialID)) {
        return false;
    }
    if (!g_gui->creatFont("Times New Roman", 18, &timeID)) {
        return false;
    }
    if (!g_gui->addStaticText(STATIC_ID_1, "Main Maenf", 400, 100, D3DCOLOR_XRGB(255, 255, 255), arialID)) {
        return false;
    }
    if (!g_gui->addStaticText(STATIC_ID_2, "Mainsddf", 400, 300, D3DCOLOR_XRGB(255, 255, 255), timeID)) {
        return false;
    }
    

    if (!g_gui->addButton(BUTTON_ID_1, 50, 170, "startUp.png", "startOver.png", "startDown.png")) {
        return false;
    }
    if (!g_gui->addButton(BUTTON_ID_2, 50, 220, "optionsUp.png", "optionsOver.png", "optionsDown.png")) {
        return false;
    }
    if (!g_gui->addButton(BUTTON_ID_3, 50, 270, "loadUp.png", "loadOver.png", "loadDown.png")) {
        return false;
    }
    if (!g_gui->addButton(BUTTON_ID_4, 50, 320, "quitUp.png", "quitOver.png", "quitDown.png")) {
        return false;
    }

	D3DXMatrixPerspectiveFovLH(&g_projection,45.0f,WINDOW_WIDTH/WINDOW_HEIGHT,0.1f,1000.0f);
	g_D3DDevice->SetTransform(D3DTS_PROJECTION,&g_projection);
	
	g_D3DDevice->SetRenderState(D3DRS_LIGHTING,FALSE);

	D3DXVECTOR3 cameraPos(0.0f,0.0f,-1.0f);
	D3DXVECTOR3 lookAtPos(0.0f,0.0f,0.0f);
	D3DXVECTOR3 upDir(0.0f,1.0f,0.0f);

	D3DXMatrixLookAtLH(&g_ViewMatrix, &cameraPos, &lookAtPos, &upDir);
	
	return true;
}

void GUICallback(int id, int state) {
    switch (id) {
        case BUTTON_ID_1:
            break;
        case BUTTON_ID_2:
            break;
        case BUTTON_ID_3:
            break;
        case BUTTON_ID_4:
            if (state == GUI_BUTTON_DOWN) {
                PostQuitMessage(0);
            }
            break;
    }
}

void RenderScene()
{
	g_D3DDevice->Clear(0,NULL,D3DCLEAR_TARGET,D3DCOLOR_XRGB(0,0,0),1.0f,0);
	g_D3DDevice->BeginScene();

	g_D3DDevice->SetTransform(D3DTS_VIEW, &g_ViewMatrix);

    processGUI(g_gui,LMBDown,mouseX,mouseY,GUICallback);

	g_D3DDevice->EndScene();

	g_D3DDevice->Present(NULL,NULL,NULL,NULL);
}

void Shutdown()
{
	if(g_D3DDevice != NULL) g_D3DDevice->Release();
	if(g_D3D != NULL) g_D3D->Release();

    if (g_gui) {
        g_gui->shutDown();
        delete g_gui;
        g_gui = NULL;
    }

	g_D3DDevice = NULL;
	g_D3D = NULL;
}
