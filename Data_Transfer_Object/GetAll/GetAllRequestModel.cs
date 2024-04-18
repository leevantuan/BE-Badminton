﻿namespace Data_Transfer_Object.GetAll
{
    public class GetAllRequestModel
    {
        public string? FilterOn { get; set; } = null;

        public string? FilterQuery { get; set; } = null;

        public string? SortBy { get; set; } = null;

        public bool IsAcsending { get; set; } = true;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 3;
    }
}
