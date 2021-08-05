# CgiProxy

Apacheとかnginxの背後にある直接叩けないAPIサーバをcgi経由で叩くproxy。
reverse proxy使えって？常にその手のが自分で設定できるとは限らないのですよ😅😅😅

(POST且つ`Content-type: application/json`以外の用途あまり考えてないです)

# 使い方
まずビルド。

```
git clone git@github.com:nenoNaninu/CgiProxy.git
cd CgiProxy/CgiProxy
dotnet publish -c Release -r linux-x64
```

上記でビルドすると、`bin/Release/net5.0/linux-x64/publish`以下に`CgiProxy`とかいう実行可能ファイルが出来上がるので、

```
cp CgiProxy index.cgi
```

などとしておく。次にpublishされたディレクトリをwwwrootが設定されているディレクトリ配下にシンボリック張っておくか、コピーしておくかする。

```
ln -s path/to/bin/Release/net5.0/linux-x64/publish www/proxy
```

これで一通り設定終わり。あとは`proxy/index.cgi`を叩いて上げればよい。

```
curl -X POST -H "Content-Type: application/json" -d '{"body":"the show must go on!"}' https://example.com/proxy/?dst=http://api.server/someapi
```

クエリパラメータとして`dst=http://api.server/some`を設定した場合、`proxy/index.cgi`が`http://api.server/someapi`に向かってボディをそのまま投げて、結果を返してくれる。
