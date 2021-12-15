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
    // �v���C���[
    public Player player;
    // �X�e�[�^�X���
    private PlayerStatus status;
    public STATUS_TYPE statusType;

    void Start()
    {
        // �v���C���[�̒��̃X�e�[�^�X�����擾
        status = player.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        // �X�e�[�^�X�����o�[�ɔ��f
        switch (statusType)
        {
            case STATUS_TYPE.HP:
                slider.maxValue = status.GetMaxHp();
                slider.value = status.GetHp();
                break;
            case STATUS_TYPE.STAMINA:
                slider.maxValue = status.GetMaxStamina();
                slider.value = status.GetStamina();
                break;
            default:
                break;
        }
    }
}
