﻿namespace MoviePoint.Models.ViewModels
{
    public class CinemanVM
    {
        public Cinema Cinema { get; set; } = new Cinema();
        public List<Movie> Movies { get; set; } =new List<Movie>();
        public int Pagination { get; set; }
        public double TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
