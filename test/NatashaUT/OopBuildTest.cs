﻿using Natasha;
using Natasha.Operator;
using System;
using Xunit;

namespace NatashaUT
{
    [Trait("快速构建", "类/结构体/接口/枚举")]
    public class OopBuildTest
    {

        [Fact(DisplayName = "Builder测试1")]
        public void TestBuilder1()
        {
            OopOperator builder = new OopOperator();
            var script = builder
                .Public.Static
                .Using<OopBuildTest>()
                .Namespace("TestNamespace")
                .OopName("TestUt1")
                .OopBody(@"public static void Test(){}")
                .PublicStaticField<string>("Name")
                .PrivateStaticField<int>("_age")
                .Script;

            Assert.Equal(@"using NatashaUT;
using System;
namespace TestNamespace{
public static class TestUt1{
public static String Name;
private static Int32 _age;
public static void Test(){}
}}", script);
            
        }




        [Fact(DisplayName = "Builder测试2")]
        public void TestBuilder2()
        {
            NAssembly assembly = new NAssembly();
            assembly.CreateStruct()
                .Namespace("TestNamespace")
                .Private.OopName("TestUt2")
                .OopBody(@"public static void Test(){}")
                .PublicStaticField<string>("Name")
                .PrivateStaticField<int>("_age");
            var result = assembly.Check();

            Assert.Equal(@"using System;
namespace TestNamespace
{
    private struct TestUt2
    {
        public static String Name;
        private static Int32 _age;
        public static void Test() { }
    }
}", result[0].Formatter);
        }




        [Fact(DisplayName = "Builder测试3")]
        public void TestBuilder3()
        {
            OopOperator builder = new OopOperator();
            var script = builder
                .Namespace<string>()
                .OopName("TestUt3")
                .ChangeToInterface()
                .Ctor(item => item
                    .StaticMember
                    .Param<string>("name")
                    .Body("this.Name=name;"))
                .OopBody(@"public static void Test(){}")
                .PublicStaticField<string>("Name")
                .PrivateStaticField<int>("_age")
                .Script;

            Assert.Equal(@"using System;
namespace System{
interface TestUt3{
public static String Name;
private static Int32 _age;
public static void Test(){}static TestUt3(String name){
this.Name=name;}
}}", script);
        }




        [Fact(DisplayName = "Builder测试4")]
        public void TestBuilder4()
        {
            OopOperator builder = new OopOperator();
            var script = builder
                .HiddenNameSpace().ChangeToEnum()
                .Public.OopName("EnumUT1")
                .EnumField("Apple")
                .EnumField("Orange")
                .EnumField("Banana")
                .Script;

            Assert.Equal($"public enum EnumUT1{{{Environment.NewLine}Apple,{Environment.NewLine}Orange,{Environment.NewLine}Banana{Environment.NewLine}}}", script);
        }



        [Fact(DisplayName = "Builder测试5")]
        public void TestBuilder5()
        {
            OopOperator builder = new OopOperator();
            var script = builder
                .HiddenNameSpace().ChangeToEnum()
                .Public.OopName("EnumUT1")
                .EnumField("Apple",1)
                .EnumField("Orange",2)
                .EnumField("Banana",4)
                .Script;

            Assert.Equal($"public enum EnumUT1{{{Environment.NewLine}Apple=1,{Environment.NewLine}Orange=2,{Environment.NewLine}Banana=4{Environment.NewLine}}}", script);
        }

    }

}