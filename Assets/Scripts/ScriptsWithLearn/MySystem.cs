//using Unity.Collections;
//using Unity.Entities;
//using Unity.Jobs;
//using Unity.VisualScripting;
//using UnityEngine;

//public partial class MySystem : SystemBase
//{
//    NativeArray<int> result = new NativeArray<int>(1, Allocator.TempJob);

//    protected override void OnCreate()
//    {
//        result[0] = 0;
//    }

//    protected override void OnUpdate()
//    {
//        //����MyJob
//        MyJob myJob = new MyJob();

//        //���������
//        myJob.num = result[0];
//        myJob.result = result;

//        //����MyJob
//        JobHandle handle = myJob.Schedule();

//        //�ȴ�MyJobִ����
//        handle.Complete();

//        //NativeArray �����и�����ָ��ͬһ�ڴ棬��������ͬһ NativeArray ���κθ����з�����ͬ�Ľ��
//        Debug.Log("The result[0] in myJob is :" + myJob.result[0]);
//        Debug.Log("The result[0] in MySystem is :" + result[0]);
//    }

//    protected override void OnDestroy()
//    {
//        //�ͷ������ڴ�
//        result.Dispose();
//    }
//}
