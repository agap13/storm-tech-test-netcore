using System.Collections.Generic;
using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, string orderField, bool hidden, string[] gravatarNames)
        {
            int i = 0;
            List<TodoItemSummaryViewmodel> todoItems = new List<TodoItemSummaryViewmodel>();
            foreach (var item in todoList.Items)
            {
                todoItems.Add(TodoItemSummaryViewmodelFactory.Create(item, gravatarNames[i]));
                i++;
            }
            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, todoItems, orderField, hidden);
        }
    }
}