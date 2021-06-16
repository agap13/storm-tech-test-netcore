using System.Collections.Generic;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }
        public string SelectedOrderBy { get; }
        public bool IsHide { get; }
        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, string selected, bool isHide)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
            SelectedOrderBy = selected;
            IsHide = isHide;
        }
    }
}