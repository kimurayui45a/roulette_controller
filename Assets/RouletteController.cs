using UnityEngine;

// �|�b�v�A�b�v�ǉ���
//  UI Toolkit ���g���Ƃ��ɕK�v�Ȑ錾
// �{�^���A���x���A�e�L�X�g�t�B�[���h�A�|�b�v�A�b�v�E�B���h�E�A�X���C�_�[�Ȃǂ�񋟂��Ă���
using UnityEngine.UIElements;


// ���̃t�@�C���́uguid�v�́u69b19b05715069b42b8364b4e2563282�v
// �i�uRoulette\Assets\RouletteController.cs.meta�v���j

// �V�[����ۑ����邱�ƂŁu�V�[����.unity�v���쐬����A�����Ɍ���̃I�u�W�F�N�g����A�b�^�b�`�󋵂��L�^�����
// ����́uRoulette\Assets\RouletteScene.unity�v�ɋL�^

// ���� RouletteController �́A�Q�[���I�u�W�F�N�g�uroulette_0�v�ɃA�^�b�`���Ă���
// �uroulette_0�v�́uRouletteScene.unity�v�ɂ�ID�u1019421225�v�Ɗ��蓖�Ă��Ă���A

// ID�u1019421225�v�����ǂ��Ă����ƁA�uMonoBehaviour:�v�ɂ�
// m_Script: {fileID: 11500000, guid: 69b19b05715069b42b8364b4e2563282, type: 3} �ƃA�^�b�`����Ă���ƋL�^������

// ����Ȋ����œ����I�ɂ�ID�Ǘ��炵��



public class RouletteController : MonoBehaviour
{
    // �y���{�ʂ�z------------------------------------------------------------------
    // ���[���b�g�̉�]���x�i�����l�F0�j
    //float rotSpeed = 0;

    //void Start()
    //{
    //    // FPS�i1�b�Ԃɉ��t���[���g�p���邩�j
    //    // 1�b�Ԃ�60�t���[�����A�܂�1�b�Ԃ�60��update()���\�b�h���Ă�
    //    // �������A�m��ł͂Ȃ��A�����̏d����@�ފ�����ł͗\��ʂ�S�Ĕ��΂��Ȃ��\��������
    //    Application.targetFrameRate = 60;

    //    // Unity�͖��t���[����ʂ�`�悵�ď�����i�߂�
    //    // Unity�����b����t���[�����X�V���悤�Ƃ��邩���w�肷��iFPS �ƌĂԁj
    //    // �X�V�p�x�i�t���[�����[�g�j��60��^�b�ɌŒ�i���b60�t���[����ڎw���i1�t���[�� �� 0.016�b�j�j
    //    // ���̐ݒ�́u�ڕW�v�ł����āu�ۏ؁v�ł͂Ȃ��A�������d�������fps�͗�����i=60�o���Ȃ��j
    //    // 30�F�x���A240�F����
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // �}�E�X�����N���b�N���ꂽ�u�Ԃɂ�锭��
    //    // 0�F���N���b�N
    //    // 1�F�E�N���b�N
    //    // 2�F���{�^���N���b�N
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // ��]���x��10�ɃZ�b�g���āA���[���b�g�̉�]���J�n�i1�t���[���A10�x�j
    //        // �uf�v�͏ȗ��\�Athis.rotSpeed = 10
    //        this.rotSpeed = 10f;
    //    }

    //    // �I�u�W�F�N�g����]�����iZ���j�� rotSpeed�x������
    //    // (0, 0, this.rotSpeed)�A(X��, Y��, Z��)�A(�c��], ����], ���v���)
    //    // �u-�i�}�C�i�X�j�v��t���邱�Ƃŉ�]�������ς��
    //    transform.Rotate(0, 0, this.rotSpeed);

    //    // ��]���x�𖈃t���[��96%�Ɍ��炷�i��4%�������j
    //    this.rotSpeed *= 0.96f;
    //}
    // �y�ȏ�A���{�ʂ�z------------------------------------------------------------------




    // �y�A�����W�z------------------------------------------------------------------

    float rotSpeed = 0;

    // ���[���b�g�̒�~�ƊJ�n���Ǘ�����t���O
    bool isSpinning = false;

    // �|�b�v�A�b�v�̕\����Ԃ��Ǘ�����t���O
    // ����́upopup.style.display = DisplayStyle.None;�v�Ŋm�F�ł��A
    // �s�v�ł��邪�A���G�ɂȂ�ꍇ�͂���ƕ֗��Ȃ̂ō쐬
    bool hasStopped = false;

    // UIDocument�FUI Toolkit �ō쐬����UI���C�A�E�g���V�[����ɕ\�����邽�߂̃R���|�[�l���g
    // UIDocument �� UI Toolkit ��p�̃R���|�[�l���g

    // UIDocument �R���|�[�l���g���󂯎�邽�߂̕ϐ�
    // Unity�G�f�B�^�̃C���X�y�N�^�[�Ŋ��蓖�Ă�
    // Unity�G�f�B�^�� UIDocument �����I�u�W�F�N�g���A�h���b�O���h���b�v�Ŋ��蓖�Ă邱�ƂŁA
    // C#������g���q�u.uxml�v�� UI �ɃA�N�Z�X�ł���
    public UIDocument uiDocument;

    // ������g���q�u.uxml�v�̃t�@�C���̒��ŁA
    // ID �� "popup" �� UI �v�f���Q�Ƃ��邽�߂̕ϐ�
    private VisualElement popup;


    void Start()
    {
        // ���߂ɁuUIDocument�����邩�m�F�v����
        // UIDocument �� null �̏�ԂŁA���̉��́uuiDocument.rootVisualElement�v�ɃA�N�Z�X����ƁA
        // ������ �uNullReferenceException�v ���������ăQ�[�����~�܂�

        // �󂯎���UIDocument�����邩�m�F�A�Ȃ��Ȃ甭�΂��Ȃ�
        if (uiDocument == null)
        {
            return;
        }

        // UIDocument�Ɋ��蓖�Ă������擾
        // rootVisualElement�FUXML�t�@�C���̃��[�g�i�ŏ�ʁj�v�f�ɃA�N�Z�X���邽�߂̃v���p�e�B
        //  UI�c���[�������I�ɒT�����ĊY���̗v�f�������邽�߃p�X�w��͕s�v
        var root = uiDocument.rootVisualElement;

        // UXML�̒��� name="popup" �������� id="popup" �Ǝw�肳�ꂽ�v�f���������A
        // popup �Ƃ����ϐ��ɑ��
        popup = root.Q<VisualElement>("popup");

        // popup�����邩�m�F�A�Ȃ��Ȃ甭�΂��Ȃ�
        if (popup == null)
        {
            return;
        }

        // �|�b�v�A�b�v�t���O���X�V
        hasStopped = false;

        // UI�v�f���\���ɂ��閽��
        // popup �Ƃ���UI�v�f�iVisualElement�j����ʂ����\���ɂ���
        popup.style.display = DisplayStyle.None;
    }


    void Update()
    {
        // Start()��UIDocument��popup�̊m�F�����Ă���̂ŁAUpdate()�ł͕s�v

        // deltaTime ���g���ꍇ�Afps �Ɋ֌W�Ȃ���葬�x�ŉ�]����

        if (Input.GetMouseButtonDown(0))
        {
            if (isSpinning)
            {
                // ��]���ɃN���b�N�A��~
                isSpinning = false;
                this.rotSpeed = 0;

                // �|�b�v�A�b�v�t���O���X�V
                hasStopped = true;

                // UI�v�f���\���ɂ��閽��
                // popup�Ƃ����v�f��\������
                popup.style.display = DisplayStyle.Flex;
            }
            else
            {
                // ��~���ɃN���b�N�A�J�n
                isSpinning = true;

                // deltaTime���g�p���邽�߁A�����ł�
                // 1�b�ԂŒB���������p�x������
                // �����1�b��360�x�Ƃ���
                this.rotSpeed = 360;

                // �|�b�v�A�b�v�t���O���X�V
                hasStopped = false;

                // UI�v�f���\���ɂ��閽��
                // popup �Ƃ���UI�v�f�iVisualElement�j����ʂ����\���ɂ���
                popup.style.display = DisplayStyle.None;
            }
        }


        if (isSpinning)
        {
            // Time.deltaTime�F�O�̃t���[������̌o�ߎ��ԁi�b�j
            transform.Rotate(0, 0, this.rotSpeed * Time.deltaTime);

            // ���b10%���炷�A1�b��ɂ�0.9�{�ɂ���
            // Mathf.Pow�F�ׂ���
            this.rotSpeed *= Mathf.Pow(0.9f, Time.deltaTime);

            // �X�s�[�h��1f��؂�����0�ɂ��Ċ��S�Ɏ~�߂�
            // ����FPS���g�p����Ȃ�uthis.rotSpeed < 0.01f�v���炢������
            if (this.rotSpeed < 50f)
            {
                this.rotSpeed = 0;
                isSpinning = false;
                hasStopped = true;
                popup.style.display = DisplayStyle.Flex;
            }
        }

    }
}
