#pragma once
#include <mscoree.h>


struct __declspec(uuid("cfe72319-45e3-4e7f-9635-c533c5d66ddf")) INetDomain : IUnknown
{
    virtual void OnCLRInit();
    virtual void MinHook_CreateInstance(LPVOID CreateHook, LPVOID RemoveHook, LPVOID EnableHook, LPVOID DisableHook);
};


class host_ctrl : public IHostControl
{
public:
    host_ctrl()
    {
        ref_count = 0;
        domain_manager = NULL;
    }
    virtual ~host_ctrl()
    {
        if (domain_manager != nullptr)
        {
            domain_manager->Release();
        }
    }
    INetDomain* get_domainmanager()
    {
        if (domain_manager)
        {
            domain_manager->AddRef();
        }
        return domain_manager;
    }
    HRESULT __stdcall GetHostManager(REFIID riid, void** ppv)
    {
        *ppv = NULL;
        return E_NOINTERFACE;
    }

    HRESULT __stdcall SetAppDomainManager(DWORD dwAppDomainID, IUnknown* pUnkAppDomainManager)
    {
        HRESULT hr = S_OK;
        hr = pUnkAppDomainManager->QueryInterface(__uuidof(INetDomain), (PVOID*)&domain_manager);
        return hr;
    }


    // required overrides by IUnknown
    HRESULT __stdcall QueryInterface(const IID& iid, void** ppv)
    {
        if (!ppv) return E_POINTER;
        *ppv = this;
        AddRef();
        return S_OK;
    }
    ULONG __stdcall AddRef()
    {
        return InterlockedIncrement(&ref_count);
    }
    ULONG __stdcall Release()
    {
        if (InterlockedDecrement(&ref_count) == 0)
        {
            delete this;
            return 0;
        }
        return ref_count;
    }
private:
    long ref_count;
    INetDomain* domain_manager;
};
