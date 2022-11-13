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
//        //创建MyJob
//        MyJob myJob = new MyJob();

//        //填充其数据
//        myJob.num = result[0];
//        myJob.result = result;

//        //调度MyJob
//        JobHandle handle = myJob.Schedule();

//        //等待MyJob执行完
//        handle.Complete();

//        //NativeArray 的所有副本都指向同一内存，您可以在同一 NativeArray 的任何副本中访问相同的结果
//        Debug.Log("The result[0] in myJob is :" + myJob.result[0]);
//        Debug.Log("The result[0] in MySystem is :" + result[0]);
//    }

//    protected override void OnDestroy()
//    {
//        //释放数组内存
//        result.Dispose();
//    }
//}
