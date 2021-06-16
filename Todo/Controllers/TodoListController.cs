using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoLists;
using Todo.Services;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserStore<IdentityUser> userStore;

        public TodoListController(ApplicationDbContext dbContext, IUserStore<IdentityUser> userStore)
        {
            this.dbContext = dbContext;
            this.userStore = userStore;
        }

        public IActionResult Index()
        {
            var userId = User.Id();
            var todoLists = dbContext.RelevantTodoLists(userId);
            var viewmodel = TodoListIndexViewmodelFactory.Create(todoLists);
            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Detail(int todoListId, string orderField="Importance", bool hidden=false)
        {
            var todoList = dbContext.SingleTodoList(todoListId);
            switch (orderField)
            {
                case "Importance":
                    todoList.Items = todoList.Items.OrderBy(x => x.Importance).ToList();
                    break;
                case "Rank":
                    todoList.Items = todoList.Items.OrderByDescending(x => x.Rank).ToList();
                    break;
                default:
                    throw new System.Exception("Unsupported sorting field");
            }
            if (hidden)
            {
                todoList.Items = todoList.Items.Where(x => x.IsDone == false).ToList();
            }

            var viewmodel = TodoListDetailViewmodelFactory.Create(todoList, orderField, hidden);

            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TodoListFields());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoListFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            var currentUser = await userStore.FindByIdAsync(User.Id(), CancellationToken.None);

            var todoList = new TodoList(currentUser, fields.Title);

            await dbContext.AddAsync(todoList);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Create", "TodoItem", new {todoList.TodoListId});
        }
    }
}