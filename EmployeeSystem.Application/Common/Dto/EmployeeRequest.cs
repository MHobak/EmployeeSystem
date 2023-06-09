﻿using EmployeeSystem.Domain.Enums;

namespace EmployeeSystem.Application.Common.Dto
{
    public record EmployeeRequest
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string RFC { get; set ; } = string.Empty;

        public DateTime BornDate { get; set; }

        public EmployeeStatus Status { get; set; } = EmployeeStatus.NotSet;
    }
}
