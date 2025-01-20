using UnityEngine;

public class Cave : Interactable
{
    [SerializeField] ModalWindow modalWindow;
    [SerializeField] ChapterFadeScreen fadeScreen;


    protected override void PerfomInteraction()
    {
        base.PerfomInteraction();

        modalWindow.ShowPromt("���������", "��� �������� ������� ����� ����. ����������?", ModalResult);

    }

    public void ModalResult(bool isConfirmed)
    {
        if (isConfirmed)
        {
            fadeScreen.EndChapter();
        }
    }
}
