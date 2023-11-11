using UnityEngine;
using PixelCrushers.DialogueSystem;

// TODO: вынести в UI
public class DoubleSpaceSubtitles : MonoBehaviour
{
    void OnConversationLine(Subtitle subtitle)
    {
        subtitle.formattedText.text += "\n";
    }
}
