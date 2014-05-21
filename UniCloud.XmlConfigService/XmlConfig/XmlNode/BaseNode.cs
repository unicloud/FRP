using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniCloud.Fleet.XmlConfigs
{

    public class BaseNode
    {
        protected List<BaseNode> _ChildNodes;
        public string Name { get; set; }
        public int Amount { get; set; }
        public BaseNode()
        {
            _ChildNodes = new List<BaseNode>();
        }

        public void AddChildNode(BaseNode Child)
        {
            _ChildNodes.Add(Child);
        }

        public void RemoveChildNode(BaseNode Child)
        {
            _ChildNodes.Remove(Child);
        }

        public List<BaseNode> ChildNodes
        {
            get { return _ChildNodes; }
        }
    }

    #region "时间节点"
    public class DateNode : BaseNode
    {
        private DateTime _Time;
        public DateTime Time { get { return _Time; } set { _Time = value; Year = _Time.Year; Month = _Time.Month; } }
        private int Year { get; set; }
        private int Month { get; set; }
    }
    #endregion

    #region "座级节点"
    public class RegionalNode : BaseNode
    {
    }
    #endregion

    #region "叶子节点"
    public class LeafNode: BaseNode
    {
        public int Percent { get; set; }
    }
    #endregion


    #region "类型节点" -- AircraftCategory
    public class TypeNode : LeafNode
    {
    }
    #endregion

    #region "分类节点" --AircraftType
    public class CategoryNode : LeafNode
    {
    }
    #endregion
}
