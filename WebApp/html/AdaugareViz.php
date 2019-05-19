<?php
mysql_connect("localhost","root","") or die ("Nu se poate
conecta la serverul MySQL");
mysql_select_db("angajati") or die("Nu se poate selecta baza
de date");

$cnp=$_POST['cnp_form'];
$nume=$_POST['nume_form'];
$prenume=$_POST['prenume_form'];
$oraAcces=$_POST['oraAcces_form']; 
$oraIesire=$_POST['oraIesire_form'];

$query=mysql_query("select count(*) from Vizitatori where CNP ='$cnp'");
$row=mysql_fetch_row($query);
$nr=$row[0];
if ($nr==0)
{
	$query1=mysql_query("insert into Vizitatori values('$cnp','$nume','$prenume','$oraAcces', '$oraIesire)");
	$query2=mysql_query("select * from Vizitatori where CNP='$cnp'"); 
	$nr_inreg=mysql_num_rows($query2); 
	if ($nr_inreg>0)
	{
		echo "<table border='2' align='center'>";
		$coln=mysql_num_fields($query2); 
		echo"<tr bgcolor='silver'>"; 
		for ($i=0; $i<$coln; $i++)
		{
		
			$var=mysql_field_name($query2,$i);
			echo "<th> $var </th>";
		}
		 echo"</tr>"; 
		 while($row=mysql_fetch_row($query2))
		{
			 echo"<tr>";
			 foreach ($row as $value)
			 {
				echo "<td>$value</td>";
			 }
			echo"</tr>";
		}
		echo"</table>";
	}
	else
	{ 
		echo"<center>";
		echo "Nu s-a gasit nici o inregistrare!!!";
		echo"</center>";
	}
}
else
{
	echo"<center>";
	echo "Vizitatorul respectiv exista deja in baza de date!";
	echo"</center>";
}
mysql_close();
?> 
