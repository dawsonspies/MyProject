using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Act1Scene1Manager : MonoBehaviour
{
    /* ACT 1
     *
     * 1)  BLACK SCREEN ~3 seconds
     * 2)  Low hum begins ~3 
     * 3)  Decompression sound
     * 3)  Lid opens (lid = all walls and ceiling of the unit)
     *     reveals the room — red emergency lighting
     * 3)  Violent alarms
     * 6)  Automated arm retracts intubation tube. slow, soft wet sound, not graphic
     * 7)  Vision is ~95% black and white
     * 8)  Camera is now active but delayed ~1-2 seconds behind
     *     input, ~30 degree range of motion, very limited
     * 9)  After ~5 seconds: input prompt appears bottom of screen
     *     no UI chrome, just white text:
     *     [E] — get up
     * 10) Sit-up animation commits fully, arms give out halfway,
     *     player falls off bed onto floor, camera hits concrete,
     *     vision pulses white once
     * 11) After ~3 seconds on floor:
     *     [E] — try again
     * 12) Player gets to knees, game does not rush, then slowly
     *     stands — camera rises to full height
     * 13) Movement now active but ~30% speed, camera sways
     *     slightly each step ~50-100f,
     * 14) Mouse no longer controls camera, controls arms instead
     *     5 things to remove:
     *       - 2x sensor pads, left forearm and left  bicep
     *       - 2x sensor pads, right forearm and right bicep
     *       - 1x IV needle, left forearm (clear dressing)
     *     click to remove, IV last, takes a
     *     moment
     * 15) Each removal returns colour incrementally — by IV out
     *     room is ~80% colour, full colour returns when camera
     *     control restores to mouse
     *
     * ============================================================
     * SCENE 1 CONTINUED: THE ROOM
     * Player can now explore — variable duration 2-10 mins
     * ============================================================
     *
     * ROOM DETAILS:
     * - 12m x 7m concrete room, 2.8m ceiling
     * - dropped acoustic tile ceiling, water stained, 2 missing,
     *   1 sagging held by single corner
     * - walls: institutional off-white paint, peeling upper
     *   corners, scuff marks at tray height along lower walls
     * - floor: grey-beige linoleum, several tiles cracked,
     *   one lifted corner, floor drain rusted shut
     * - 8x fluorescent fixtures: 3 dead, 2 flickering, 3 lit
     * - 2x red emergency lights, warm incandescent, wire cage
     *
     * SEDATION UNITS (x8):
     * - brushed aluminium exterior, dull with age
     * - white medical foam interior, yellowed
     * - Garrick's unit: body impression permanent in foam
     * - status LED panel above each: 6 dark, 1 corrupted
     *   (resolves and corrupts on loop), 1 flatlining (Garrick's)
     * - nameplate holder above each:
     *     5x legible printed names
     *     1x scratched out above open unit (not Garrick's)
     *     1x scratched out above Garrick's unit
     *     1x scratched out Garrick's unit
     * - open unit tray still extended, interior clean, exterior
     *   dust disturbed where seal broke recently
     *
     * INTERACTIVE OBJECTS:
     * - corrupted monitoring unit: player can examine, readout
     *   never stabilises, no explanation given
     * - laminated sign above door:
     *     "SOTERIUS FACILITY — LONG-TERM SEDATION WARD
     *      UPON WAKING: REMAIN CALM.
     *      REMOVAL OF SENSORS SHOULD BE PERFORMED SLOWLY.
     *      IF EXPERIENCING MEMORY FRAGMENTATION, THIS IS NORMAL.
     *      REPORT TO MEDICAL UPON EXIT."
     *     handwritten in marker along bottom edge, small:
     *     "last checked: 2121 — M.R."
     *
     * 1)  Player approaches door (~2m away)
     *     all alarms cut simultaneously — total sudden silence
     *     red lights remain
     * 2)  Speaker grill above door plays pre-recorded voice —
     *     real human, recorded early in facility's life, slightly
     *     too cheerful, like a voicemail greeting playing forever:
     *     "Good morning. All systems report crew status
     *      within acceptable parameters."
     * 3)  2 second silence
     * 4)  Alarms resume — different, lower frequency, more
     *     sustained, structural rather than administrative,
     *     louder than before (scare the player)
     * 5)  Garrick grips door frame — hands visible, door grinds, concrete dust falls,
     *     opens a bit after ~3 seconds of effort
     * 6)  Gap reveals hallway — dark except red light far end
     *     and flickering strip light out of frame
     * 7)  Garrick goes through sideways
     *
     * ============================================================
     * END OF SCENE 1
     * ============================================================
     */

    [Header("Action")]
    [SerializeField] private int actionCount = 1; //Just for display for now

    [Header("UI References")]
    [SerializeField] private GameObject blackScreen;

    [Header("Audio References")]
    [SerializeField] private AudioSource alarm1;
    [SerializeField] private AudioSource alarm2;
    [SerializeField] private AudioSource alarm3;

    [Header("References")]
    [SerializeField] private PM_PlayerMovement playerController;
    [SerializeField] private PM_CameraController cameraController;
    [SerializeField] private PM_PlayerAudio audioController;

    [Header("Cam Vars")]
    [SerializeField] private float camShakePercent = 50f;
    [SerializeField] private float camUpperLookLimit = -10f;
    [SerializeField] private float camLowerLookLimit = 85f;

    [Header("Player Vars")]
    [SerializeField] private float playerShakePercent = 100f;

    private void Start()
    {
        blackScreen.SetActive(true);
        NextAction();
        StartIntroSequence();

        playerController.SetInputLock(true);
        playerController.SetShakey(playerShakePercent);

        cameraController.shakeyMode = true;
        cameraController.SetShakey(camShakePercent);

        cameraController.SetLookLimits(camUpperLookLimit, camLowerLookLimit);
    }

    private void StartIntroSequence()
    {
        StartCoroutine(Wait(3f));
        NextAction();
        blackScreen.SetActive(false);
    }

    IEnumerator Wait(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
    }

    private void NextAction()
    {
        actionCount++;
    }

    /* 
     * 
     * 
     * WAIT FOR ANIM TO BE OVER!!!
     *  using System.Collections;
        using UnityEngine;

        public class AnimationWaiter : MonoBehaviour
        {
            private Animator animator;

            void Start()
            {
                animator = GetComponent<Animator>();
                StartCoroutine(PlayAndWait("AttackState", 0)); // 0 is the Base Layer index
            }

            IEnumerator PlayAndWait(string stateName, int layerIndex)
            {
                // 1. Play the animation
                animator.Play(stateName);

                // 2. CRITICAL: Wait 1 frame so the Animator transitions to the new state
                yield return null;

                // 3. Extract the exact duration of the newly active state
                float animationLength = animator.GetCurrentAnimatorStateInfo(layerIndex).length;

                // 4. Halt execution for that exact timeframe
                yield return new WaitForSeconds(animationLength);

                // 5. Code placed here executes exactly when the animation finishes
                Debug.Log("Animation finished playing!");
            }
        }

     * 
     * 
     */
}
