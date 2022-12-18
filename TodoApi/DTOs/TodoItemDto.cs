using TodoApi.Models;

namespace TodoApi.DTOs {

    public class TodoItemGetDto {

        public long Id { get; set; }

        public string? Name { get; set; }

        public bool IsComplete { get; set; }

        public static TodoItemGetDto ToDto(TodoItem item) {
            return new TodoItemGetDto {
                Id= item.Id,
                Name= item.Name,
                IsComplete= item.IsComplete,
            };
        }
    }

    public class TodoItemPostDto {

        public string? Name { get; set; }

        public bool IsComplete { get; set; }

        public static TodoItem ToItem(TodoItemPostDto dto) {
            return new TodoItem {
                Name = dto.Name,
                IsComplete = dto.IsComplete,
            };
        }
    }
}
