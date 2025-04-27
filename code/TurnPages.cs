using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
public class TurnPages : MonoBehaviour
{
    public GameObject debugReader;
    public InputActionReference nextPageAction;
    public InputActionReference previousPageAction;
    public int toppage;

    public bool porradesce;
    public float distanciaEntrePaginas;
    public bool caralhovolta;
    public bool porravira;
    private float progresso = 0.0f;

    public float inconsistenciadoangulo = 20.0f;
    public string tag;

    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;

    public string path;
    public int LastPageRendered;
    public int TotalPages;
    private int comecinho = -1;
    private RenderPages renderPages;
    private bool isAnimating = false;

    public float delay;
    // Start is called before the first frame update
    void TurnNextPage()
    {
        //if (porravira) return;

        isAnimating = true;
        //mudar a posição de todos os últimos conjuntos de páginas para a posição da próxima página
        //GameObject page0 = GameObject.Find("page_" + toppage);
        //percebi que deveria criar a dorsal do livro quando criar suas páginas, se fosse criar agora cada dorsal teria um transform
        //diferente (baseado no transform do livro quando a página foi criada) e isso ia dar caquinha
        //createFatherSpine(page0);
        GameObject spine = transform.Find("spine_page_" + toppage)?.gameObject;
        turnSpineAround(spine);
        if (toppage != 0)
        {
            Debug.Log("descendo essa porra");
            for (int i = 0; i < toppage; i = i + 2)
            {
                moveCertainPageVertical(i);
            }
        }
        //RunAnimation();
        toppage += 2;
        StartCoroutine(ResetAnimationFlagAfterDelay(delay)); // Adjust the delay to match the animation duration

    }

    IEnumerator ResetAnimationFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAnimating = false;
    }
    void getBackAround()
    {
        for (int i = 0; i < toppage; i = i + 2)
        {
            moveCertainPageVertical(i, -1f);
        }
        toppage -= 2;
        GameObject spine = transform.Find("spine_page_" + toppage)?.gameObject;
        turnSpineAround(spine, 0f);
        Debug.Log("subindo essa porra");
        // Adjust the delay to match the animation duration

    }

    void moveCertainPageVerticalByObject(GameObject pages)
    {
        Animation anim = pages.AddComponent<Animation>();
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        Renderer renderer = pages.GetComponent<Renderer>();
        Vector3 currentPosition = pages.transform.localPosition;


        AnimationCurve curveX = AnimationCurve.Linear(0.0f, currentPosition.x, 1.0f, currentPosition.x);
        //lembrar de trocar esse 0.04f no finalzinho para um número menor (a distância entre as páginas, provavelmente 0.001f ou algo assim)
        AnimationCurve curveY = AnimationCurve.Linear(0.0f, pages.transform.localPosition.y, 1.0f, pages.transform.localPosition.y - distanciaEntrePaginas * 0.01f * inconsistenciadoangulo);
        AnimationCurve curveZ = AnimationCurve.Linear(0.0f, currentPosition.z, 1.0f, currentPosition.z);

        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        clip.SetCurve("", typeof(Transform), "localPosition.z", curveZ);

        anim.AddClip(clip, "Move");
        anim.Play("Move");


        Debug.Log("tabom, agora vou destruir a animação");
        Destroy(anim, clip.length + 0.1f);


    }

    void moveCertainPageVertical(int pagenumber, float direcao = 1f)
    {
        GameObject pages = transform.Find("spine_page_" + pagenumber)?.gameObject;
        Animation anim = pages.AddComponent<Animation>();
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        Renderer renderer = pages.GetComponent<Renderer>();
        Vector3 currentPosition = pages.transform.localPosition;

        Debug.Log("currentPosition: " + pages.transform.localPosition.y);

        AnimationCurve curveX = AnimationCurve.Linear(0.0f, currentPosition.x, 1.0f, currentPosition.x);
        //lembrar de trocar esse 0.04f no finalzinho para um número menor (a distância entre as páginas, provavelmente 0.001f ou algo assim)
        AnimationCurve curveY = AnimationCurve.Linear(0.0f, pages.transform.localPosition.y, 1.0f, pages.transform.localPosition.y - distanciaEntrePaginas * 0.01f * inconsistenciadoangulo * direcao);
        AnimationCurve curveZ = AnimationCurve.Linear(0.0f, currentPosition.z, 1.0f, currentPosition.z);

        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        clip.SetCurve("", typeof(Transform), "localPosition.z", curveZ);

        Animator animator = pages.GetComponent<Animator>();
        if (animator == null)
        {
            animator = pages.AddComponent<Animator>();
        }
        animator.applyRootMotion = true;
        anim.AddClip(clip, "Move");
        anim.Play("Move");

        Debug.Log("currentPosition: " + (pages.transform.localPosition.y - distanciaEntrePaginas * inconsistenciadoangulo));
        Debug.Log("tabom, agora vou destruir a animação");
        Destroy(anim, clip.length + 0.1f);


    }

    public void corrigindoAPosicaoPara180GrausTmjDog(GameObject pages)
    {
        //O unity acredita que esse método não tem um "receiver" (não tem um objeto que ele está chamando), mas o método aparentemente está funcionando então
        //vamos deixar assim mesmo. Tomara q isso não volte para me morder a bunda
        Debug.Log("Corrigindo a posição para 180 graus");
        pages.transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    void RunAnimation()
    {
        /* for (int i = 0; i < toppage; i = i + 2)
        {
            GameObject lastpage = GameObject.Find("page_" + i);
            Animation animlast = lastpage.AddComponent<Animation>();
            AnimationClip cliplast = new AnimationClip();
            cliplast.legacy = true;
            Renderer rendererlast = lastpage.GetComponent<Renderer>();
            //mover para o lugar certo.
            progresso += distanciaEntrePaginas * 0.01f;
            Debug.Log("Progresso: " + progresso);
            AnimationCurve curveY = AnimationCurve.Linear(0.0f, lastpage.transform.localPosition.y, 1.0f, lastpage.transform.localPosition.y - progresso);

            cliplast.SetCurve("", typeof(Transform), "localPosition.y", curveY);
            animlast.AddClip(cliplast, "Move");
            animlast.Play("Move");
            gameObject.transform.position = new Vector3(0.20923f, gameObject.transform.position.y, gameObject.transform.position.z);
            Destroy(animlast, cliplast.length);
        }
        progresso = 0.0f;*/

        GameObject page0 = GameObject.Find("page_" + toppage);
        Animation anim = page0.AddComponent<Animation>();
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        Renderer renderer = page0.GetComponent<Renderer>();
        Vector3 size = renderer.bounds.size;

        AnimationCurve curveX = AnimationCurve.Linear(0.0f, 0, 1.0f, 0.2002f);
        //progresso += distanciaEntrePaginas * 0.01f
        //AnimationCurve curveY = AnimationCurve.Linear(0.0f, 0, 1.0f, - progresso + distanciaEntrePaginas * 0.01f);

        // por que o z retorna em 177 graus e não 180?
        AnimationCurve curveRotZ = AnimationCurve.Linear(0.0f, page0.transform.rotation.eulerAngles.z, 1.0f, page0.transform.rotation.eulerAngles.z - 180f);

        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        //c
        clip.SetCurve("", typeof(Transform), "localEulerAngles.z", curveRotZ);

        Animator animator = page0.GetComponent<Animator>();
        if (animator == null)
        {
            animator = page0.AddComponent<Animator>();
        }
        animator.applyRootMotion = true;

        // Add and play the animation

        AnimationEvent animEvent = new AnimationEvent();
        animEvent.time = clip.length;
        animEvent.functionName = "corrigindoAPosicaoPara180GrausTmjDog";
        animEvent.objectReferenceParameter = page0;
        clip.AddEvent(animEvent);

        anim.AddClip(clip, "MoveAndRotate");
        anim.Play("MoveAndRotate");
        Destroy(anim, clip.length + 0.1f); // Remove the component after the animation finishes
        Destroy(animator, clip.length + 0.1f);
        if (toppage != 0)
        {
            for (int i = 0; i < toppage; i = i + 2)
            {
                moveCertainPageVertical(i);
            }
        }
    }



    void turnSpineAround(GameObject spine, float angle = -180.0f)
    {
        isAnimating = true;
        Debug.Log("Spine: " + spine.name);
        Debug.Log("Spine: " + spine.transform.eulerAngles);
        //spine.transform.localRotation = Quaternion.Euler(0, 0, angle);
        //quaternion pra  0 0 -180 funciona.

        //vou ter que refazer esse detalhe
        //essa caralha mistura radianos e graus, então cuidado 
        Animation anim = spine.GetComponent<Animation>();
        if (anim == null)
        {
            anim = spine.AddComponent<Animation>();
        }
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        Renderer renderer = spine.GetComponent<Renderer>();
        Debug.Log(spine.transform.parent.gameObject);
        AnimationCurve curveRotX = AnimationCurve.Linear(0.0f, 0, 1.0f, 0f);
        AnimationCurve curveRotZ = AnimationCurve.Linear(0.0f, -180 - angle, 1.0f, angle);
        AnimationCurve curveRotY = AnimationCurve.Linear(0.0f, 0, 1.0f, 0f);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.x", curveRotX);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.y", curveRotY);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.z", curveRotZ);
        anim.AddClip(clip, "Rotate");
        anim.Play("Rotate");
        Destroy(anim, clip.length + 0.1f);

        //lugar inadequado entretanto o código não funciona muito bem fora desse método

        //spine.transform.Rotate(spine.transform.parent.gameObject.transform.eulerAngles.x, spine.transform.parent.gameObject.transform.eulerAngles.y, spine.transform.eulerAngles.z + angle);
        //moveCertainPageVerticalByObject(spine);
        StartCoroutine(ResetAnimationFlagAfterDelay(delay));
    }

    // Update is called once per frame
    void controles()
    {
        /*if (page0 != null)
        {
            Debug.Log("Found Page_0");
        }
        else
        {
            Debug.Log("Page_0 not found");
        }*/
    }
    void updateRender()
    {
        if (LastPageRendered < TotalPages - 2)
        {
            renderPages.LoadTexturesForThePage(LastPageRendered + 1, LastPageRendered + 2, path, gameObject, debugReader);
            LastPageRendered += 2;
            //so pra testar ai que besteira

            renderPages.UnloadTexturesForThePage(comecinho - 2, comecinho, gameObject, debugReader);
            comecinho += 2;
        }
    }
    void updateRenderGoingBack()
    {
        renderPages.UnloadTexturesForThePage(LastPageRendered + 1, LastPageRendered + 2, gameObject, debugReader);
        renderPages.LoadTexturesForThePage(comecinho - 2, comecinho, path, gameObject, debugReader);

        LastPageRendered -= 2;
        comecinho -= 2;
    }
    void Start()
    {
        renderPages = GetComponent<RenderPages>();
    }
    void Update()
    {
        if (IsBeingGrabbed())
        {
            //debugReader.GetComponent<TextMeshPro>().text = "Estou sendo agarrado";

            if (porravira && IsBeingGrabbed() && !isAnimating)
            {
                updateRender();
                debugReader.GetComponent<TextMeshPro>().text += "Page turned ->";
                TurnNextPage();
                porravira = false;
            }
            if (caralhovolta && toppage > 0 && IsBeingGrabbed() && !isAnimating)
            {
                updateRenderGoingBack();
                debugReader.GetComponent<TextMeshPro>().text += "<- Page returned";
                Debug.Log("Vou voltar essa porra");
                getBackAround();
                caralhovolta = false;
            }
        }
        else
        {
            //debugReader.GetComponent<TextMeshPro>().text += "Não estou sendo agarrado";
            //porravira = false;
            //caralhovolta = false;
        }
        if (porradesce)
        {
            moveCertainPageVertical(toppage);
            porradesce = false;
        }
    }

    private void Awake()
    {
        nextPageAction.action.Enable();
        nextPageAction.action.performed += OnNextPagePerformed;

        previousPageAction.action.Enable();
        previousPageAction.action.performed += OnPreviousPagePerformed;

        grabInteractable = GetComponent<XRGrabInteractable>();
    }
    private void OnDestroy()
    {
        nextPageAction.action.performed -= OnNextPagePerformed;
        nextPageAction.action.Disable();

        previousPageAction.action.performed -= OnPreviousPagePerformed;
        previousPageAction.action.Disable();
    }
    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        debugReader.GetComponent<TextMeshPro>().text = "I'm not being grabbed";
        isGrabbed = false;
    }
    public bool IsBeingGrabbed()
    {
        return isGrabbed;
    }
    private void OnNextPagePerformed(InputAction.CallbackContext context)
    {
        if (IsBeingGrabbed() && !porravira) porravira = true;
    }

    private void OnPreviousPagePerformed(InputAction.CallbackContext context)
    {
        if (IsBeingGrabbed() && !caralhovolta) caralhovolta = true;
    }
}
