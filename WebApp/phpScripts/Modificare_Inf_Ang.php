<?php
$marca=$_POST['Marca'];
$nume = $_POST['Nume'];
$prenume = $_POST['Prenume'];
$CNP = $_POST['CNP'];
//$Poza = $_POST['Poza'];
$divizia = $_POST['Divizia'];
$acces_perm = $_POST['Access'];


$servername = "db4free.net";
$username = "dragonfly97";
$password = "DragonFly123";
$dbname = "dragonfly";

// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}

if($nume != "")
{
	$sql = "UPDATE SET Nume='$nume' WHERE Marca='$marca'"
}

if($prenume != "")
{
	$sql = "UPDATE SET Prenume='$prenume' WHERE Marca='$marca'"
}

if($CNP != "")
{
	$sql = "UPDATE SET CNP='$CNP' WHERE Marca='$marca'"
}

if($divizia != "")
{
	$sql = "UPDATE SET Divizia='$divizia' WHERE Marca='$marca'"
}

if($acces_perm != "")
{
	$sql = "UPDATE SET Access='$acces_perm' WHERE Marca='$marca'"
}

if (substr($marca, 0, 1) == "P")
{
	if($nume != "")
	{
		$sql = "UPDATE SET Nume='$nume' WHERE Marca='$marca'"
	}

	if($prenume != "")
	{
		$sql = "UPDATE SET Prenume='$prenume' WHERE Marca='$marca'"
	}

	if($CNP != "")
	{
		$sql = "UPDATE SET CNP='$CNP' WHERE Marca='$marca'"
	}
}

if (mysqli_query($conn, $sql)) {
    echo "Record updated successfully";
} else {
    echo "Error updating record: " . mysqli_error($conn);
}

mysqli_close($conn);
?>