namespace MetBench_DAL
{
    //MetamorphciRelation与Application的中间类
    public class MetamorphicRelationApplication
    {
        // MetamorphicRelation的外键
        public int IdMR { get; set; }

        // Application的外键
        public string ApplicationName { get; set; }
    }
}
