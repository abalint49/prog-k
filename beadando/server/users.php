<?php
if ($_SERVER['REQUEST_METHOD'] == 'GET') {
  $stmt = $pdo->prepare('SELECT name,email,admin FROM users');
  $stmt->execute();
  $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
  return;
}

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
  $data = json_decode(file_get_contents('php://input'));

  if ($_GET['users'] == 'login') {
    $stmt = $pdo->prepare('SELECT id, name, admin FROM users WHERE name = ? AND password = ?');
    $stmt->execute([$data->name, md5($data->password)]);
    $data = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$data) {
      $data = null;
    }
    return;
  }
}