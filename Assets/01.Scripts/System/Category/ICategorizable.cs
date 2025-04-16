using System;

public interface ICategorizable
{
    Enum Category { get; }         // 어떤 enum이든 상관없이
    string DisplayName { get; }    // 셀에 보여줄 이름
}