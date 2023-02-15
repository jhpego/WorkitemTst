using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkitemTst.Controllers;
using WorkitemTst.Entitys;
using WorkitemTst.Models;
using static Extensions.MyExtension;

namespace WorkitemTst.Controllers
{
    [Route("api/[controller]")]
    public class ListsController : Controller
    {


        readonly AppDBContext _appDBContext;
        public ListsController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }


        [HttpGet("relationtypes")]
        public ActionResult<IEnumerable<TypeViewModel>> GetRelationTypes()
        {
            var typeList = Enum.GetValues(typeof(WorkitemRelationKind))
                           .Cast<WorkitemRelationKind>()
                           .Select(t => new TypeViewModel
                           {
                               Id = (int)t,
                               Name = t.ToString()
                           });
            return typeList.ToList();
        }


        [HttpGet("interactions")]
        public ActionResult<IEnumerable<TypeViewModel>> GetInteractions()
        {
            var interactions = _appDBContext.Iteration
                .Select(iteraction => new TypeViewModel()
                {
                    Id = (int)iteraction.Id,
                    Name = iteraction.Name,
                });
            return interactions.ToList();

            //var nodes = 
            var rootNode = _appDBContext.Iteration.Where(itr => itr.ParentId == null).FirstOrDefault();

            var treeNode = new TreeNode<Iteration>()
            {
                //ParentId = rootNode.ParentId,
                Id = rootNode.Id,
                Node = rootNode
            };

            //var tree = ProcessNode(rootNode);

            //var nodes = _appDBContext.Iteration.ToList().Select(iter => new TreeNode<TypeViewModel>
            //{
            //    ParentId = iter.ParentId,
            //    Node = new TypeViewModel()
            //    {
            //        Id = iter.Id,
            //        Name = iter.Name
            //    },
            //    Children = GetIteractionChildren(iter.Id).Select(
            //        child => new TypeViewModel()
            //        {
            //            Id = child.Id,
            //            Name = child.Name
            //        }
            //        ).ToList()
            //});

            //var nodeList = nodes.ToList();

            //return new List<TypeViewModel>();

            ;

            //_appDBContext.Iteration.GetAsyncEnumerator

               //var res =  GetChildren(_appDBContext.Iteration, 1);
            //var tree = nodes.Select(item =>
            //{
            //    item.Children = nodes.Where(child => child.ParentId.HasValue && child.ParentId == item.Id).ToList();
            //    return item;
            //})
            // .Where(item => !item.ParentId.HasValue)
            // .ToList();
        }


        //private void PrintTree(IEnumerable<TreeNode<TypeViewModel>> nodes, int indent = 0)
        //{
        //    foreach (var root in nodes)
        //    {
        //        Console.WriteLine(string.Format("{0}{1}", new String('-', indent), root.Id));
        //        PrintTree(root.Children, indent + 1);
        //    }
        //}
        //private List<T> GetIteractionChildren<T>( int? parentId) where T : BaseEntity
        //{

        //    if (parentId == null)
        //    {
        //        return new List<T>();
        //    }
        //    //using(var dbContext = _appDBContext)
        //    //{
        //    var iteractions = _appDBContext.Iteration.Where(itr => itr.ParentId == parentId);
        //        return (List<T>) iteractions.ToList();
        //    //}


            
        //}


        //private TreeNode<Treeable> ProcessNode(Treeable node)
        //{

        //    var children = _appDBContext.Iteration.GetChildren(node.Id);

        //    return new TreeNode<Treeable>()
        //    {
        //        //ParentId = node.ParentId,
        //        Node = node,
        //        Children = _appDBContext.Iteration.GetChildren(node.Id)
        //    };
        //}


    }




    class TreeNode<T> { 
        public IEnumerable<TreeNode<T>> Children { get; set; }
        //public int? ParentId { get; set; }

        public int? Id { get; set; }
        public T Node { get; set; }
    }


}


namespace Extensions {
    public static class MyExtension


    {


        public class Treeable : BaseEntity
        {
            //public int Id { get; set; }
            public int ParentId { get; set; }
        }


        public static List<B> GetChildren<B>(this DbSet<B> source, int parentId) where B : Treeable
        {
            if (parentId == null)
            {
                return new List<B>();
            }
            var iteractions = source.Where(itr => itr.ParentId == parentId);
            return iteractions.ToList();
        }
    }

}







