using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBench_IDAL
{
    public class MRIntelligentRecommendations
    {
        //程序
        public string Program { get; set; }
        //相似度
        public string Similarity { get; set; }
        //输入模式
        public string InputPattern { get; set; }//非空字段 r
        //输出模式
        public string OutputPattern { get; set; }//非空字段 R
        //输入模式Late渲染图片路径
        public string InputPatternImagepath { get; set; }
        //输出模式Latex渲染图片路径
        public string OutputPatternImagepath { get; set; }
    }
}
