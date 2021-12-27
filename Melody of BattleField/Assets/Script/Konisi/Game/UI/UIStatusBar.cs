using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusBar : MonoBehaviour
{
    // �X�e�[�^�X���
    public enum STATUS_TYPE
    {
        HP,
        STAMINA
    };

    // �������o�[
    public Slider slider;
    // �v���C���[�I�u�W�F�N�g
    private GameObject playerObject;
    // �v���C���[
    public Player player;
    // �X�e�[�^�X���
    private PlayerStatus status;
    public STATUS_TYPE statusType;

    void Start()
    {
        playerObject = GameObject.Find("Keyboard");
        player = playerObject.GetComponent<Player>();
        // �v���C���[�̒��̃X�e�[�^�X�����擾
        status = player.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        // �X�e�[�^�X�����o�[�ɔ��f
        switch (statusType)
        {
            case STATUS_TYPE.HP:
                slider.maxValue = status.MaxHp;
                slider.value = status.Hp;
                break;
            case STATUS_TYPE.STAMINA:
                slider.maxValue = status.MaxStamina;
                slider.value = status.Stamina;
                break;
            default:
                break;
        }
    }
}
