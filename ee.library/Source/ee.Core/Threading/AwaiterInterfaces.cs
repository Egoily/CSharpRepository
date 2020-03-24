using System.Runtime.CompilerServices;

namespace ee.Core.Threading
{
    /// <summary>
    /// ��ʾһ���ɵȴ��������һ���������ش����͵�ʵ������˷�������ʹ�� `await` �첽�ȴ���
    /// </summary>
    /// <typeparam name="TAwaiter">���ڸ� await ȷ������ʱ���� IAwaiter ��ʵ����</typeparam>
    public interface IAwaitable<out TAwaiter> where TAwaiter : IAwaiter
    {
        /// <summary>
        /// ��ȡһ�������� await �ؼ����첽�ȴ����첽�ȴ�����
        /// �˷����ᱻ�������Զ����á�
        /// </summary>
        TAwaiter GetAwaiter();
    }

    /// <summary>
    /// ��ʾһ����������ֵ�Ŀɵȴ��������һ���������ش����͵�ʵ������˷�������ʹ�� `await` �첽�ȴ�����ֵ��
    /// </summary>
    /// <typeparam name="TAwaiter">���ڸ� await ȷ������ʱ���� IAwaiter{<typeparamref name="TResult"/>} ��ʵ����</typeparam>
    /// <typeparam name="TResult">�첽���صķ���ֵ���͡�</typeparam>
    public interface IAwaitable<out TAwaiter, out TResult> where TAwaiter : IAwaiter<TResult>
    {
        /// <summary>
        /// ��ȡһ�������� await �ؼ����첽�ȴ����첽�ȴ�����
        /// �˷����ᱻ�������Զ����á�
        /// </summary>
        TAwaiter GetAwaiter();
    }

    /// <summary>
    /// ���ڸ� await ȷ���첽���ص�ʱ����
    /// </summary>
    public interface IAwaiter : INotifyCompletion
    {
        /// <summary>
        /// ��ȡһ��״̬����״̬��ʾ�����첽�ȴ��Ĳ����Ѿ���ɣ��ɹ���ɻ������쳣������״̬�ᱻ�������Զ����á�
        /// ��ʵ���У�Ϊ�˴ﵽ����Ч�����������Ӧ����ֵ������ʼ��Ϊ true������ʼ��Ϊ false��
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// �˷����ᱻ�������� await ����ʱ�Զ������Ի�ȡ����״̬�������쳣����
        /// </summary>
        void GetResult();
    }

    /// <summary>
    /// ��ִ�йؼ����루�˴����еĴ�����ܸ�Ӧ�ó����е�����״̬��ɸ���Ӱ�죩ʱ��
    /// ���ڸ� await ȷ���첽���ص�ʱ����
    /// </summary>
    public interface ICriticalAwaiter : IAwaiter, ICriticalNotifyCompletion
    {
    }

    /// <summary>
    /// ���ڸ� await ȷ���첽���ص�ʱ��������ȡ������ֵ��
    /// </summary>
    /// <typeparam name="TResult">�첽���صķ���ֵ���͡�</typeparam>
    public interface IAwaiter<out TResult> : INotifyCompletion
    {
        /// <summary>
        /// ��ȡһ��״̬����״̬��ʾ�����첽�ȴ��Ĳ����Ѿ���ɣ��ɹ���ɻ������쳣������״̬�ᱻ�������Զ����á�
        /// ��ʵ���У�Ϊ�˴ﵽ����Ч�����������Ӧ����ֵ������ʼ��Ϊ true������ʼ��Ϊ false��
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// ��ȡ���첽�ȴ������ķ���ֵ���˷����ᱻ�������� await ����ʱ�Զ������Ի�ȡ����ֵ�������쳣����
        /// </summary>
        /// <returns>�첽�����ķ���ֵ��</returns>
        TResult GetResult();
    }

    /// <summary>
    /// ��ִ�йؼ����루�˴����еĴ�����ܸ�Ӧ�ó����е�����״̬��ɸ���Ӱ�죩ʱ��
    /// ���ڸ� await ȷ���첽���ص�ʱ��������ȡ������ֵ��
    /// </summary>
    /// <typeparam name="TResult">�첽���صķ���ֵ���͡�</typeparam>
    public interface ICriticalAwaiter<out TResult> : IAwaiter<TResult>, ICriticalNotifyCompletion
    {
    }
}
