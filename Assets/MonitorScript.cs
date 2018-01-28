using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonitorScript : Singleton<MonitorScript> {

    public TextMeshPro text;

    public int currentIndex = 0;
    public string currentText;
    public string textToWrite;

    public void AppendText(string txt)
    {
        textToWrite += txt+"\n";
    }
    public void ResetText()
    {
        this.text.text = "Launch!";
        currentText = "";
        this.AppendText("Launch!");
    }
    float lastUpdate = 0;
	void Update ()
    {
        if (this.textToWrite.Length > 0)
        {
            if (lastUpdate == 0 || Time.time - lastUpdate < 0.5f)
            {
                lastUpdate = Time.time;
                var cArray = this.textToWrite.ToCharArray();
                if(this.currentIndex< cArray.Length)
                {
                    currentText += cArray[this.currentIndex];
                    this.text.text = currentText;
                    
                    if (this.text.renderedHeight > this.text.rectTransform.sizeDelta.y)
                    {
                        this.text.alignment = TextAlignmentOptions.BottomLeft;
                    }
                    if (this.currentIndex < cArray.Length)
                        this.currentIndex++;
                }
                
            }
        }
		
	}
}
