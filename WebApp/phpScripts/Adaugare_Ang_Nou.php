<?php
$marca=$_POST['Marca'];
$nume = $_POST['Nume'];
$prenume = $_POST['Prenume'];
$CNP = $_POST['CNP'];
//$Poza = $_POST['Poza'];
$divizia = $_POST['Divizia'];
$acces_perm = $_POST['Access'];


$servername = "localhost";
$username = "username";
$password = "password";
$dbname = "myDB";

// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}

$sql = "INSERT INTO table_name (Marca, Nume, Prenume, CNP, Divizia, Access) VALUES ('$marca', '$nume', '$prenume', '$CNP', '$divizia', '$acces_perm')";

//Introducere in subtabele pentru fiecare tip de angajat in parte ... codificare 'marca' necesara 

if (substr($marca, 0, 1) == "")
{
	$sql_1 = "INSERT INTO table_name_1 (Marca, Nume, Prenume, CNP) VALUES ('$marca', '$nume', '$prenume', '$CNP')";
}

if (substr($marca, 0, 1) == "")
{
	$sql_1 = "INSERT INTO table_name_2 (Marca, Nume, Prenume, CNP) VALUES ('$marca', '$nume', '$prenume', '$CNP')";
}

if (substr($marca, 0, 1) == "")
{
	$sql_1 = "INSERT INTO table_name_3 (Marca, Nume, Prenume, CNP) VALUES ('$marca', '$nume', '$prenume', '$CNP')";
}

if (mysqli_query($conn, $sql) && mysqli_query($conn, $sql_1))
{
    echo "New record created successfully";
} else {
    echo "Error: " . $sql . "<br>" . mysqli_error($conn);
}



mysqli_close($conn);
?>