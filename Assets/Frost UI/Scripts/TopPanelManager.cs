using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Michsky.UI.Frost
{
    public class TopPanelManager : MonoBehaviour
    {
        [Header("PANEL LIST")]
        public List<GameObject> panels = new List<GameObject>();

        [Header("EXTRA PANEL LIST")]
        public List<GameObject> extraPanels = new List<GameObject>();

        [Header("BUTTON LIST")]
        public List<GameObject> buttons = new List<GameObject>();

        // [Header("PANEL ANIMS")]
        private string panelFadeIn = "Panel In";
        private string panelFadeOut = "Panel Out";

        // [Header("BUTTON ANIMS")]
        private string buttonFadeIn = "Hover to Pressed";
        private string buttonFadeOut = "Pressed to Normal";

        private GameObject currentPanel;
        private GameObject nextPanel;

        private GameObject currentButton;
        private GameObject nextButton;

        [Header("SETTINGS")]
        public int currentPanelIndex = 0;
        private int currentButtonlIndex = 0;

        private Animator currentPanelAnimator;
        private Animator nextPanelAnimator;

        private Animator currentButtonAnimator;
        private Animator nextButtonAnimator;

        private Queue<int> backQueue = new Queue<int>();
        private int currentExPanelIndex = -1;

        void Start()
        {
            currentButton = buttons[currentPanelIndex];
            currentButtonAnimator = currentButton.GetComponent<Animator>();
            currentButtonAnimator.Play(buttonFadeIn);

            currentPanel = panels[currentPanelIndex];
            currentPanelAnimator = currentPanel.GetComponent<Animator>();
            currentPanelAnimator.Play(panelFadeIn);
                        
        }

        public void PanelAnim(int newPanel)
        {
            if (newPanel != currentPanelIndex)
            {
                if(currentExPanelIndex >= 0)
                {
                    currentPanel = extraPanels[currentExPanelIndex];
                    backQueue.Clear();
                    currentExPanelIndex = -1;
                    Debug.Log("Expanel");
                }
                else
                {
                    currentPanel = panels[currentPanelIndex];
                }

                currentPanelIndex = newPanel;
                nextPanel = panels[currentPanelIndex];

                PlayPanelAnim(currentPanel, nextPanel);

                //currentPanelAnimator = currentPanel.GetComponent<Animator>();
                //nextPanelAnimator = nextPanel.GetComponent<Animator>();

                //currentPanelAnimator.Play(panelFadeOut);
                //nextPanelAnimator.Play(panelFadeIn);

                //button
                currentButton = buttons[currentButtonlIndex];

                currentButtonlIndex = newPanel;
                nextButton = buttons[currentButtonlIndex];

                currentButtonAnimator = currentButton.GetComponent<Animator>();
                nextButtonAnimator = nextButton.GetComponent<Animator>();

                currentButtonAnimator.Play(buttonFadeOut);
                nextButtonAnimator.Play(buttonFadeIn);
            }
        }

        private void PlayPanelAnim(GameObject currPanel, GameObject nextPanel)
        {
            Animator currAnim = currPanel.GetComponent<Animator>();
            Animator nextAnim = nextPanel.GetComponent<Animator>();

            currAnim.Play(panelFadeOut);
            nextAnim.Play(panelFadeIn);
        }

        public void ExtraPanelAnim(int nextExPanelIndex)
        {
            GameObject currPanel = null, nextPanel = null;
            if(currentExPanelIndex < 0)
            {
                currPanel = panels[currentPanelIndex];
                nextPanel = extraPanels[nextExPanelIndex];

                currentExPanelIndex = nextExPanelIndex;

            }
            else
            {
                if(currentExPanelIndex != nextExPanelIndex)
                {
                    //int currExPanelIndex = backQueue.Peek();
                    currPanel = extraPanels[currentExPanelIndex];
                    nextPanel = extraPanels[nextExPanelIndex];

                    backQueue.Enqueue(currentExPanelIndex);
                    currentExPanelIndex = nextExPanelIndex;
                }
               
            }

            if(currPanel != null && nextPanel != null)
            {
                PlayPanelAnim(currPanel, nextPanel);
            }

        }

        public void BackExtraPanel()
        {
            GameObject currPanel = null, previousPanel = null;
            if (backQueue.Count != 0)
            {
                int previousIndex = backQueue.Dequeue();
                currPanel = extraPanels[currentExPanelIndex];
                previousPanel = extraPanels[previousIndex];
                currentExPanelIndex = previousIndex;
            }
            else
            {
                currPanel = extraPanels[currentExPanelIndex];
                previousPanel = panels[currentPanelIndex];
                currentExPanelIndex = -1;
            }

            if (currPanel != null && previousPanel != null)
            {
                PlayPanelAnim(currPanel, previousPanel);
            }
        }

    }
}