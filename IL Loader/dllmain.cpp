// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <iostream>
#include <metahost.h>
#include <CorError.h>
#pragma comment(lib, "mscoree.lib")
#include <sstream>

#include "host.h"
#include "include/MinHook.h"
#pragma comment(lib, "libMinHook-x64-v141-mdd.lib")

void create_hook(LPVOID pTarget, LPVOID pDetour, LPVOID* ppOriginal)
{
    LPVOID orig;
    auto result = MH_CreateHook(pTarget, pDetour, &orig);
    if (result != MH_OK) {
        std::cout << "[CLRBootstrap] create hook failed, status: " << MH_StatusToString(result) << std::endl;
        return;
    }
    *ppOriginal = orig;
}

void remove_hook(LPVOID pTarget)
{
    auto result = MH_RemoveHook(pTarget);
    if (result != MH_OK) {
        std::cout << "[CLRBootstrap] remove hook failed, status: " << MH_StatusToString(result) << std::endl;
        return;
    }
}

void enable_hook(LPVOID pTarget)
{
    auto result = MH_EnableHook(pTarget);
    if (result != MH_OK) {
        std::cout << "[CLRBootstrap] enable hook failed, status: " << MH_StatusToString(result) << std::endl;
        return;
    }
}

void disable_hook(LPVOID pTarget)
{
    auto result = MH_DisableHook(pTarget);
    if (result != MH_OK) {
        std::cout << "[CLRBootstrap] disable hook failed, status: " << MH_StatusToString(result) << std::endl;
        return;
    }
}


int main()
{
    MH_Initialize();

    ICLRMetaHost* meta_host = NULL;
    ICLRRuntimeInfo* runtime_info = NULL;
    ICLRRuntimeHost* runtime_host = NULL;
    AllocConsole();
    CLRCreateInstance(CLSID_CLRMetaHost, IID_ICLRMetaHost, ((LPVOID*)&meta_host));
    meta_host->GetRuntime(L"v4.0.30319", IID_ICLRRuntimeInfo, (LPVOID*)&runtime_info);
    runtime_info->GetInterface(CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, (LPVOID*)&runtime_host);
    ICLRControl* clr_control = nullptr;
    runtime_host->GetCLRControl(&clr_control);

    host_ctrl* host_control = host_control = new host_ctrl();
    runtime_host->SetHostControl(host_control);
    clr_control->SetAppDomainManagerType(L"WL", L"WorldLoader.Init.Interface");

    runtime_host->Start();

    INetDomain* net_domain = host_control->get_domainmanager();
    net_domain->MinHook_CreateInstance(
        create_hook,
        remove_hook,
        enable_hook,
        disable_hook);
    net_domain->OnCLRInit();

    runtime_info->Release();
    meta_host->Release();
    runtime_host->Release();
    net_domain->Release();

    return 0;
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
        case DLL_PROCESS_ATTACH:
            main(); // This is just basic textbook use of CoreCLR and Minhook, Provided By Unix <3
        break;
        case DLL_THREAD_ATTACH:
        case DLL_THREAD_DETACH:
        case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

