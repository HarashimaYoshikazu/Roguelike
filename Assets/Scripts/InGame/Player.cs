using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField,Tooltip("������HP�ϐ�")]
    int _initHP = 40;
    public int InitHP => _initHP;

    [SerializeField, Tooltip("�������g�����X�t�H�[��")]
    Transform _initTransform;

    List<ISkill> _skillList = new List<ISkill>();

    List<ISkill> _pasiveList = new List<ISkill>();

    [SerializeField,Tooltip("�e�̃v���n�u")]
    BulletController _bulletController = null;
    [SerializeField, Tooltip("�e�̐e�I�u�W�F�N�g")]
    Transform _bulletParent = null;

    [SerializeField,Tooltip("�ߐڍU���̓����蔻��I�u�W�F�N�g")]
    Net[] _weapons = null;
    public Net[] Weapons => _weapons;

    GenericObjectPool<BulletController> _bulletPool;
    public GenericObjectPool<BulletController> BulletPool => _bulletPool;

    [SerializeField]
    AudioSource _audio = null;
    [SerializeField]
    AudioClip[] _clips = null;

    private void Awake()
    {        
        _bulletPool = new GenericObjectPool<BulletController>(_bulletController,_bulletParent);
        GameManager.Instance.SetPlayer(this);
        AddSkill(2);
    }


    private void Update()
    {
        if(!GameManager.Instance.IsPauseFlag)
        {
            foreach (var skill in _skillList)
            {
                skill.Update();
            }
        }
    }

    public void AddSkill(int skillId)
    {
        var alraedyHaving = _skillList.Where(s => s.SkillId == (SkillDef)skillId); //�����Ŏ����SkillID�ƍ��v����X�L���𒊏o
        if (alraedyHaving.Count() > 0)
        {
            alraedyHaving.Single().LevelUp(); //���o�����X�L���͈�������肦�Ȃ�����Single�֐����g�p�����x���A�b�v
        }
        else
        {
            ISkill newSkill = null; 

            switch ((SkillDef)skillId)�@//���߂ẴX�L����new���ă��X�g�ɒǉ�
            {
                case SkillDef.NetAttack:
                    newSkill = new NetSkill();
                    break;
                case SkillDef.Bullet:
                    newSkill = new BulletSkill();
                    break;
                case SkillDef.SpeedUp:
                    newSkill = new SpeedPassive();
                    break;
                case SkillDef.Heal:
                    newSkill = new Heal();
                    break;
            }

            if (newSkill != null)
            {
                newSkill.SetUp();
                _skillList.Add(newSkill);
            }
        }
    }

    public void InitPos()
    {
        this.transform.position = _initTransform.position;
    }

    public void ResetSkill()
    {
        _skillList.Clear();
    }

    public void AudioPlay(int value)
    {
        _audio.PlayOneShot(_clips[value]);
    }
}
