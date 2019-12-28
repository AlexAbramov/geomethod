Description
===========

The library introduces the new classes:

GmProviderFactory - wraps DbProviderFactory
GmConnection - wraps DbConnection
GmCommand - wraps DbCommand

Sample
======

GmProviderFactory fact=new SqlServerProvider("Integrated Security=yes; Server=Acer\\SqlServer2000;");
using(GmConnection conn=fact.CreateConnection())
{
  GmCommand cmd=conn.CreateCommand("insert into Employees values(@Age,@Name)");
  cmd.AddInt("Id",25);
  cmd.AddString("Name","John");
  cmd.ExecuteNonQuery();
}

