using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Button button;
    public static string selectedFileName;

    private void Start()
    {
        // Gắn sự kiện cho nút button
        button.onClick.AddListener(OnButtonClick);

        // Tạo danh sách tên file JSON cho dropdown
        PopulateDropdown();
    }

    private void PopulateDropdown()
    {

        // Ví dụ:
        string[] fileNames = GetJSONFileNames();

        // Xóa tất cả các mục cũ trong dropdown
        dropdown.ClearOptions();

        if (fileNames.Length > 0)
        {
            // Tạo danh sách các mục cho dropdown từ danh sách tên file
            //dropdown.AddOptions(fileNames.ToList());
            dropdown.AddOptions(fileNames.ToList().Select(x => new TMP_Dropdown.OptionData(x)).ToList());
            // Đặt giá trị mặc định cho dropdown là mục đầu tiên
            dropdown.value = 0;
        }
    }

    private void OnButtonClick()
    {
        // Lấy tên file được chọn từ dropdown
        string seFileName = dropdown.options[dropdown.value].text;
        //Debug.Log("DropdownManager: " + seFileName);
        // Gán tên file đã chọn vào biến selectedFileName trong script GameManager
        DropdownManager.selectedFileName = seFileName + ".json";
        //Debug.Log("DropdownManager: " + DropdownManager.selectedFileName);

        // SceneManager.LoadScene("TênSceneChơiMap");
        Application.LoadLevel("Play");

    }


    // từ thư mục lưu trữ của bạn
    private string[] GetJSONFileNames()
    {
        // Lấy danh sách tên file JSON từ thư mục lưu trữ
        // Ví dụ:
        string[] fileNames = Directory.GetFiles(Application.persistentDataPath, "*.json");
        for (int i = 0; i < fileNames.Length; i++)
        {
            // Xóa đường dẫn và phần mở rộng của tên file
            fileNames[i] = Path.GetFileNameWithoutExtension(fileNames[i]);
        }

        return fileNames;
    }
}
