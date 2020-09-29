using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using Sensiks;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Security.Policy;

public class CreateNewProject : MonoBehaviour
{
    SerializableSavedata data;

    private string videoPath;
    private string thumbnailPath;

    private bool copyThumbDone = false;
    private bool copyVideoDone = false;

    private string filename;

    public Text statusText;

    public GameObject overlay;

    public InputField titleInput;

    public VideoPlayer player;

    private string _status = "";
    public string status 
    {
        get 
        {
            return _status;
        }
        set 
        {
            _status = value;
            statusText.text = _status;
        }
    }

    public GameObject CloseOverlayButton;

    private void Start()
    {
        data = new SerializableSavedata();
    }

    public void SetTitle(string title) 
    {
        data.projectName = title;
    }

    public void SetVideo(string path) 
    {
        videoPath = path;
    }

    public void SetThumbnail(string path) 
    {
        thumbnailPath = path;
    }

    public void CreateProject() 
    {
        data.projectName = titleInput.text;

        if (string.IsNullOrEmpty(videoPath)) 
        {
            //todo: show message
            Debug.Log("no video found");
        }
        if (string.IsNullOrEmpty(data.projectName)) 
        {
            //todo: show message
            Debug.Log("no project name found");
        }

        if(string.IsNullOrEmpty(videoPath) || string.IsNullOrEmpty(data.projectName)) 
        {
            return;
        }
        else 
        {
            overlay.SetActive(true);
        }

        status = "Creating Project files...";
        string name = GetRandomName();
        //make sure name isn't taken already
        while (File.Exists(Application.streamingAssetsPath + "/Saves/Savedata/" + name + ".json"))
        {
            name = GetRandomName();
        }

        filename = name;

        string toPathVideo = Application.streamingAssetsPath + "/Saves/Videos/" + name + ".mp4";
        string toPathThumbnail = Application.streamingAssetsPath + "/Saves/Thumbnails/" + name + ".jpg";

        string pathToData = Application.streamingAssetsPath + "/Saves/Data/" + name + ".dat";

        if (!File.Exists(pathToData))
        {
            File.Create(pathToData);
        }

        status = "Copying files...";

        data.pathToData = pathToData;
        
        if (!string.IsNullOrEmpty(thumbnailPath))
        {
            StartCoroutine(Copy(thumbnailPath, toPathThumbnail, false));
            data.pathToThumbnail = toPathThumbnail;
            StartCoroutine(waitForCopyThumbnail());
        }
        else 
        {
            copyThumbDone = true;
        }

        if (!string.IsNullOrEmpty(videoPath))
        {
            StartCoroutine(Copy(videoPath, toPathVideo, true));
            data.pathToVideo = toPathVideo;
            StartCoroutine(waitForCopyVideo());
        }           
    }

    private IEnumerator Copy(string frompath, string topath, bool isVideo)
    {
        byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
        bool cancelFlag = false;

        using (FileStream source = new FileStream(frompath, FileMode.Open, FileAccess.Read))
        {
            long fileLength = source.Length;
            using (FileStream dest = new FileStream(topath, FileMode.CreateNew, FileAccess.Write))
            {
                long totalBytes = 0;
                int currentBlockSize = 0;

                while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    totalBytes += currentBlockSize;
                    double percentage = (double)totalBytes * 100.0 / fileLength;

                    dest.Write(buffer, 0, currentBlockSize);

                    cancelFlag = false;
                    OnProgressChanged(percentage, ref cancelFlag);
                    yield return null;
                    if (cancelFlag == true)
                    {
                        // Delete dest file here
                        break;
                    }
                }
            }
        }
        if (isVideo)
        {
            copyVideoDone = true;
        }
        else 
        {
            copyThumbDone = true;
        }
    }

    private IEnumerator waitForCopyVideo() 
    {
        yield return new WaitUntil(() => copyVideoDone == true);
        videosCopied();
    }

    private IEnumerator waitForCopyThumbnail()
    {
        yield return new WaitUntil(() => copyVideoDone == true);
        videosCopied();
    }

    private IEnumerator LoadVideo() 
    {
        player.Prepare();
        while (!player.isPrepared) 
        {
            yield return null;
        }

        data.videoLenght = Mathf.RoundToInt((float)player.length);

        SaveData();
    }

    private void videosCopied() 
    {
        if(copyThumbDone && copyVideoDone) 
        {
            status = "Checking video lenght...";
            
            string url = data.pathToVideo;
            player.source = VideoSource.Url;
            player.url = url;

            StartCoroutine(LoadVideo());
        }
    }

    private void SaveData() 
    {
        status = "Saving project files...";
        SavesManager.Instance.SaveSavedata(data);
        SavesManager.Instance.SetSelectedData(data);
        status = "Done!";
        CloseOverlayButton.SetActive(true);
    }

    private void OnProgressChanged(double percentage, ref bool cancel) 
    {
        Debug.Log(Mathf.Round((float)percentage));   
    }

    private string GetRandomName() 
    {
        System.Random random = new System.Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}