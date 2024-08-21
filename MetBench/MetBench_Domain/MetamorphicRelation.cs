using LiteDB;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using static System.Net.Mime.MediaTypeNames;

namespace MetBench_Domain
{   //蜕变关系类
    public class MetamorphicRelation 
    {
        [BsonId]
        public int IdMR { get; set; }//非空字段
        public string Description { get; set; }
        public string Context { get; set; }
        public string Constraint { get; set; }
        public string OrderOfMR { get; set; }//非空字段
        public RtType RepresentationType { get; set; }//非空字段
        public string InputPattern { get; set; }//非空字段 r
        public string OutputPattern { get; set; }//非空字段 R

        #region
        //Latex转sympy格式
        public string InputPatterntosympy { get; set; }//非空字段
        public string OutputPatterntosympy { get; set; }//非空字段
        //InputPatternimage属性
        public byte[] InputPatternImageData { get; set; }//非空字段
        //OutputPatternimage属性
        public byte[] OutputPatternImageData { get; set; }//非空字段
        #endregion

        public string DimensionOfInputPattern { get; set; }//非空字段
        public string DimensionOfOutputPattern { get; set; } //非空字段

        //Application的Name 作为外键
        public string ApplicationName { get; set; } = String.Empty; //Litedb不存储空字符串  AppllicationName之间用:隔开
    }
}