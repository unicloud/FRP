#region �汾��Ϣ

// ========================================================================
// ��Ȩ���� (C) 2013 UniCloud 
//�����๦�ܸ�����
// 
// ���ߣ�zhangnx ʱ�䣺2014/2/10��13:11
// ������FRP
// ��Ŀ��PoolNotRunException
// �汾��V1.0.0
// 
// �޸��ߣ� ʱ�䣺 
// �޸�˵����
// ========================================================================

#endregion

using System;

namespace UniCloud.DataService.Connection
{
    /// <summary>
    ///     ����δ����
    /// </summary>
    public class PoolNotRunException : Exception
    {
        public PoolNotRunException()
            : base("����δ����")
        {
        }

        public PoolNotRunException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     �����Ѿ����л���δ��ȫ����
    /// </summary>
    public class PoolNotStopException : Exception
    {
        public PoolNotStopException()
            : base("�����Ѿ����л���δ��ȫ����")
        {
        }

        public PoolNotStopException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ���ӳ���Դδȫ������
    /// </summary>
    public class ResCallBackException : Exception
    {
        public ResCallBackException()
            : base("���ӳ���Դδȫ������")
        {
        }

        public ResCallBackException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ���ӳ��Ѿ����ͣ������ṩ����
    /// </summary>
    public class PoolFullException : Exception
    {
        public PoolFullException()
            : base("���ӳ��Ѿ����ͣ������ṩ����")
        {
        }

        public PoolFullException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ����״̬����
    /// </summary>
    public class StateException : Exception
    {
        public StateException()
            : base("����״̬����")
        {
        }

        public StateException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     һ��key����ֻ������һ������
    /// </summary>
    public class KeyExecption : Exception
    {
        public KeyExecption()
            : base("һ��key����ֻ������һ������")
        {
        }

        public KeyExecption(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     �޷��ͷţ������ڵ�key
    /// </summary>
    public class NotKeyExecption : Exception
    {
        public NotKeyExecption()
            : base("�޷��ͷţ������ڵ�key")
        {
        }

        public NotKeyExecption(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ��ǰ���ӳ�״̬�����Զ����Ը�ֵ
    /// </summary>
    public class SetValueExecption : Exception
    {
        public SetValueExecption()
            : base("��ǰ���ӳ�״̬�����Զ����Ը�ֵ")
        {
        }

        public SetValueExecption(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ������Χ����
    /// </summary>
    public class ParameterBoundExecption : Exception
    {
        public ParameterBoundExecption()
            : base("������Χ����")
        {
        }

        public ParameterBoundExecption(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ��Ч��ConnTypeEnum���Ͳ���
    /// </summary>
    public class ConnTypeExecption : Exception
    {
        public ConnTypeExecption()
            : base("��Ч��ConnTypeEnum���Ͳ���")
        {
        }

        public ConnTypeExecption(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ������Դ�ľ��������ķ���ʱ����
    /// </summary>
    public class OccasionExecption : Exception
    {
        public OccasionExecption()
            : base("������Դ�ľ��������ķ���ʱ����")
        {
        }

        public OccasionExecption(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ������Դ�Ѿ�ʧЧ��
    /// </summary>
    public class ResLostnExecption : Exception
    {
        public ResLostnExecption()
            : base("������Դ�Ѿ�ʧЧ��")
        {
        }

        public ResLostnExecption(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ������Դ�����Ա����䡣
    /// </summary>
    public class AllotExecption : Exception
    {
        public AllotExecption()
            : base("������Դ�����Ա����䡣")
        {
        }

        public AllotExecption(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ������Դ�Ѿ������䲢�Ҳ������ظ����á�
    /// </summary>
    public class AllotAndRepeatExecption : AllotExecption
    {
        public AllotAndRepeatExecption()
            : base("������Դ�Ѿ������䲢�Ҳ������ظ�����")
        {
        }

        public AllotAndRepeatExecption(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     ���ü����Ѿ�Ϊ0��
    /// </summary>
    public class RepeatIsZeroExecption : Exception
    {
        public RepeatIsZeroExecption()
            : base("���ü����Ѿ�Ϊ0��")
        {
        }

        public RepeatIsZeroExecption(string message)
            : base(message)
        {
        }
    }
}