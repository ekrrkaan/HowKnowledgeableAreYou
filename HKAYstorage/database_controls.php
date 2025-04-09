<?php
$baglanti = new mysqli("localhost","root","","hkay_db");

// Check connection
if ($baglanti -> connect_errno) {
  echo "Failed to connect to MySQL: " . $baglanti -> connect_error;
  exit();
}


if($_POST['unity']=="register"){
	
$nickname = $_POST['nickname'];

$sorgu="insert into accounts(nickname,skor) Values('$nickname',0)";

$sorguSonuc = $baglanti-> query($sorgu);

if($sorguSonuc){
	echo "Registeration Successfull";
} else
	echo "Please choose different nickname";
}





if($_POST['unity'] == 'sorular'){
	
	$sorgu="select * from sorular";
$sorguSonuc=$baglanti->query($sorgu);

if($sorguSonuc->num_rows > 0 ){
	
	
	$butunSatirlar=array();
    while($row=$sorguSonuc->fetch_object()){
	
	array_push($butunSatirlar,array(
	
	"soru"=>$row->soru,
	"a_Cevabi"=>$row->a_Cevabi,
	"b_Cevabi"=>$row->b_Cevabi,
	"c_Cevabi"=>$row->c_Cevabi,
	"d_Cevabi"=>$row->d_Cevabi,
	"dogruCevap"=>$row->dogruCevap,
	"saniye"=>$row->saniye
	
	)
	);
}
echo json_encode(array("butunSorular"=>$butunSatirlar));	
}
else{
	//HATA
 }
}

if($_POST['unity']=="skorumuGetir"){
	
$nickname = $_POST['nickname'];

$sorgu="select * from accounts where nickname='$nickname'";

$sorguSonuc = $baglanti-> query($sorgu);

if($sorguSonuc){
	echo $sorguSonuc->fetch_object()->skor;
} else
	echo "Skor getirilemedi";
}

if($_POST['unity']=="skorumuGuncelle"){
	
$nickname = $_POST['nickname'];
$skor = $_POST['skor'];

$sorgu="update accounts set skor='$skor' where nickname='$nickname'";

$sorguSonuc = $baglanti-> query($sorgu);

if($sorguSonuc){
	echo " Güncellendi ";
} else
	echo "Güncellenemedi";
}

if($_POST['unity'] == 'skorlariGetir'){
	
	$sorgu="SELECT * FROM accounts ORDER BY skor DESC";
$sorguSonuc=$baglanti->query($sorgu);

if($sorguSonuc->num_rows > 0 ){
	
	
	$butunSatirlar=array();
    while($row=$sorguSonuc->fetch_object()){
	
	array_push($butunSatirlar,array(
	
	"KullaniciNickname"=>$row->nickname,
	"KullaniciSkor"=>$row->skor
	)
	);
}
echo json_encode(array("butunSkorlar"=>$butunSatirlar));	
}
else{
	//HATA
 }
}

if($_POST['unity']=="SendQuestion"){
	
$soru = $_POST['question'];
$a_Cevabi = $_POST['answer1'];
$b_Cevabi = $_POST['answer2'];
$c_Cevabi = $_POST['answer3'];
$d_Cevabi = $_POST['answer4'];
$dogruCevap = $_POST['correctanswer'];
$saniye = $_POST['time'];

$sorgu = "INSERT INTO sorular (soru, a_Cevabi, b_Cevabi, c_Cevabi, d_Cevabi, dogruCevap, saniye) VALUES ('$soru', '$a_Cevabi', '$b_Cevabi', '$c_Cevabi', '$d_Cevabi', '$dogruCevap', '$saniye')";
$sorguSonuc = $baglanti-> query($sorgu);

if($sorguSonuc){
	echo "Insert to table successfull";
} else
	echo "Inserting not able";
}


$baglanti->close();
	

?>