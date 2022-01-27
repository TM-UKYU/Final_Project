using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    // �v���C���[�I�u�W�F�N�g
    private GameObject playerObject;
    // �v���C���[
    public Player player;
    // �X�e�[�^�X���
    private PlayerStatus status;
    // �����A�C�e��
    public ItemManager itemManager;
    // �������̃A�C�e���擾�p
    public ChangeEquipItem changeEquipItem;

    private void Start()
    {
        // ����ɂ���ĕύX
        switch (StageInformation.Weapon)
        {
            case StageInformation.WEAPON_ID.KEYBOARD: playerObject = GameObject.Find("Keyboard"); break;
            case StageInformation.WEAPON_ID.GUITAR: playerObject = GameObject.Find("Guitar"); break;
            default: break;
        }

        player = playerObject.GetComponent<Player>();
        // �v���C���[�̒��̃X�e�[�^�X�����擾
        status = player.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        // �X�e�[�^�X��ύX
        if(Input.GetKeyDown(KeyCode.E))
        {
            switch(changeEquipItem.centerArrayNum)
            {
                case 0:
                    if (itemManager.GetItemNum(ItemManager.Item.HEAL) > 0)
                    {
                        if (status.Hp != status.MaxHp)
                        {
                            // Hp��
                            status.Hp += 60;
                            // Hp������𒴂��Ȃ��悤��
                            if(status.Hp >= status.MaxHp) { status.Hp = status.MaxHp; }
                            // �A�C�e������
                            itemManager.ConsumptionItem(ItemManager.Item.HEAL);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
