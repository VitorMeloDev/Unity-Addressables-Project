using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// Used for the Hat selection logic
public class PlayerConfigurator : MonoBehaviour
{
    [SerializeField] private Transform m_HatAnchor;
    [SerializeField] private GameObject m_HatInstance;

    private AsyncOperationHandle<GameObject> m_HatLoadOpHandle;

    void Start()
    {           
        LoadInRandomHat();
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            Destroy(m_HatInstance);
            Addressables.ReleaseInstance(m_HatLoadOpHandle);

            LoadInRandomHat();
        }
    }

    public void LoadInRandomHat()
    {
        int randomIndex = Random.Range(0, 6);
        string hatAddress = string.Format("Hat{0:00}", randomIndex);

        m_HatLoadOpHandle = Addressables.LoadAssetAsync<GameObject>(hatAddress);
        m_HatLoadOpHandle.Completed += OnHatLoadComplete;
    }

    private void OnHatLoadComplete(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        m_HatInstance = Instantiate(asyncOperationHandle.Result, m_HatAnchor);
    }

    private void OnDisable()
    {
        m_HatLoadOpHandle.Completed -= OnHatLoadComplete;
    }
}
