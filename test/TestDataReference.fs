module TestDataReference

let expected = """
check-mcg-128-rxs-m-64.out:  64bit: 0xd5f68603b0924460 0x03758f56e7333bba 0x200332c8ce428792 0xac73c844a78a0ceb 0x97f6b0dfbb526d08 0x8049945ca1844832
check-mcg-128-rxs-m-64.out:  Coins: HHHTHTHTTTTTTHTTTTHTHHHTTTTHTTTTHTTHTHHTTHTHHHTHHHTHTTHHTHTHTTHTH
check-mcg-128-rxs-m-64.out:  Rolls: 1 3 1 5 2 6 6 6 1 6 3 3 4 3 4 4 6 6 3 1 1 4 2 1 2 5 4 4 2 4 4 3 1
check-mcg-128-rxs-m-64.out:  Cards: 9c 7s 2s Jd Kd 3h 4c Js 5h 9d 8c 7d Ah 2d 8h Tc 6h Ad 6c 8d Ts Kc Qh As 4s 2c 9s 5s 7c Jc 9h Qd Qc 6s 2h 3d Qs 5d 7h 5c 6d Kh Jh Ks 4h Th 4d Ac Td 3s 8s 3c
check-mcg-128-xsh-rr-64.out:  64bit: 0x1f3f7121e50db375 0x5d18374c90e01618 0x98a20fad7ae65b50 0xb04a7f95ed7e29f9 0xa535567f80452bb4 0xb46ef40ccacf759f
check-mcg-128-xsh-rr-64.out:  Coins: TTTTHTHTHHTHTHTTTTTTTHHHTTTHTTTHTHHHHTHHHHHHHTHHTHTTTHHHHTHTHHTHT
check-mcg-128-xsh-rr-64.out:  Rolls: 5 4 3 4 2 2 5 1 1 2 6 5 1 1 1 4 5 2 4 6 6 1 5 1 1 5 4 3 5 3 1 1 3
check-mcg-128-xsh-rr-64.out:  Cards: Jd 8s 5c Qc 8c Td 7s 7c Kh Ks 9s Ts 4s Js Th Qh 9c 6c 5h 3c 3s 9h As Kc Tc 7d 5d 7h Jc 3h 6h Kd 2c 4d 4c Ac 8d Ad 6d 8h Ah 3d 2d 2s 5s 2h Qd 4h 6s Jh 9d Qs
check-mcg-128-xsh-rs-64.out:  64bit: 0x147cfdc564f926ea 0xcba306e86a91167e 0xbd732f506128cc74 0xfcaf63b37a27fdef 0xbb4a5353f6062772 0xccacf5d36acae471
check-mcg-128-xsh-rs-64.out:  Coins: TTHTHTTTHHTTTTHTHTTTHTTTTHHHTTHTTHHHHHTTTTTTHHTHHTTHTTTTHHHHHHTTH
check-mcg-128-xsh-rs-64.out:  Rolls: 4 5 1 1 5 1 6 2 4 1 3 5 2 2 1 2 4 5 4 6 3 1 3 2 3 6 1 4 5 1 3 5 4
check-mcg-128-xsh-rs-64.out:  Cards: 7h 3s 2c Kh 9d 5s 3d Qs 8d 7s 7c Td Ac Jh Kd Jd 8s 4d 6c As 5c 4c Qh 4h 3h 6d 8c 2s Js 5h Kc 4s 2h 9c Qd 6s 9h 6h 5d 2d 7d Th Ks Tc 8h Ts 3c 9s Ah Jc Qc Ad
check-mcg-128-xsl-rr-64.out:  64bit: 0x63b4a3a813ce700a 0x382954200617ab24 0xa7fd85ae3fe950ce 0xd715286aa2887737 0x60c92fee2e59f32c 0x84c4e96beff30017
check-mcg-128-xsl-rr-64.out:  Coins: HTTTTTTTTTTTHHTHHHHHHTTHTHTTHTTTTTHTTHTTHHHHTHHTHHHHHTTTHHHHHHHHH
check-mcg-128-xsl-rr-64.out:  Rolls: 4 5 5 4 4 4 5 4 3 1 6 1 6 6 2 6 2 2 3 3 2 5 6 4 2 6 4 4 3 2 4 2 6
check-mcg-128-xsl-rr-64.out:  Cards: Jh Jc 2h 4h Kd 2c 4d 5c Kc 8c 7h Td 9s 4s 2s Jd 8s 6h Qd 6d 9c Qh Tc 3h 7c 2d 4c 3s Qc 5s 8d 9d Ah Ac 3c 6s 5h 7s Ks Th 5d Ad Js Kh 9h Qs 6c 3d 7d 8h As Ts
check-mcg-16-rxs-m-8.out:   8bit: 0xa2 0xbd 0x3b 0x85 0xe7 0xe5 0x40 0x60 0x19 0xf1 0x60 0xef 0xc1 0x14
check-mcg-16-rxs-m-8.out:  Coins: TTTTTHTHTHHTTTHHTHTTHTHTTHTHTTTHTTHTHTTTTTTHTTTTHHHHTHTTTHHHHHTTH
check-mcg-16-rxs-m-8.out:  Rolls: 3 2 6 2 3 2 3 3 3 6 3 5 6 3 1 1 5 6 4 2 4 5 3 4 4 3 5 5 2 4 1 6 3
check-mcg-16-rxs-m-8.out:  Cards: Jc 9c Ad Ah 6h Ts Th Kd 8h 2h 8s 5s 4h 9h 3s Qs 8c 3c 6s Js 2s Kc 7c 4s 7h Qh 3d 5c Ac 8d Qd Jd 3h As 6c Kh 4c Qc 7d Ks 9s 7s 2d 2c 5h Tc 4d Td 5d 9d Jh 6d
check-mcg-16-xsh-rr-8.out:   8bit: 0x01 0x89 0x03 0x01 0x4a 0xd8 0x70 0x33 0xd2 0xa5 0x7b 0x5e 0x47 0x0b
check-mcg-16-xsh-rr-8.out:  Coins: HHTTTHTHTHTHHTTHTHHTTTHTTHTTHTTTTHHHHHTTTHTTHHHHTHHTTTHHHHTHTTHHH
check-mcg-16-xsh-rr-8.out:  Rolls: 3 6 6 3 6 5 3 1 5 3 1 3 6 1 5 2 1 2 2 5 5 5 2 5 6 3 3 4 3 6 6 4 6
check-mcg-16-xsh-rr-8.out:  Cards: As Qs Jc Kc Qh Jd 3d 4d 4h 7h 8h 2h 6d 8s 2s Qc Ts 5c 7c 6s 6c 4c 5s Ks 9s Jh Js 7s 8d 2d 8c Kh 3h 9c Ad Td Ah Th 4s 5h 7d 9h Tc 2c 5d Kd Qd Ac 6h 3s 3c 9d
check-mcg-16-xsh-rs-8.out:   8bit: 0x05 0xa0 0x40 0x21 0x21 0xe9 0x10 0xbf 0xac 0x0d 0x72 0x8c 0xc8 0x41
check-mcg-16-xsh-rs-8.out:  Coins: TTTTHTTTHHHTHHTHHTTHTTHHTHHTTHTHTTTHHTTHTHTTTHHTHHHHTTHHHHTHHHTTT
check-mcg-16-xsh-rs-8.out:  Rolls: 2 4 1 4 6 6 4 6 6 4 3 3 1 5 2 2 2 4 1 2 4 4 4 3 3 5 3 1 2 5 3 4 4
check-mcg-16-xsh-rs-8.out:  Cards: 6c As 2h 3s 5c Ks 2s 4d 2c Tc Kc Th 5h 6s 8c Js 9c Jh 8h Ts Qh 9h Ad Td Qc 9d 7s Qd 8d Jd 4s 9s 5s Ah Qs 7h 4c Ac 3h 8s Kh 3c 2d 6h 7c 4h Jc 5d Kd 6d 7d 3d
check-mcg-32-rxs-m-16.out:  16bit: 0xa6e4 0xb96a 0x2ef3 0x8f84 0xe80b 0x2cd5 0x6d0b 0x0011 0xc913 0xc0b4
check-mcg-32-rxs-m-16.out:  Coins: HHHTTTTHTTHHHTTTHTHHHTHHHTTTHHTTHTHTTTTHTTHHHTTTHTTTHHTHHHTHHHHHT
check-mcg-32-rxs-m-16.out:  Rolls: 3 6 6 3 6 5 3 6 4 6 1 1 3 3 5 3 2 2 4 5 4 2 6 2 4 6 2 1 6 6 2 2 3
check-mcg-32-rxs-m-16.out:  Cards: 5h Js Jc Ac 9d Tc 5s 4d Kc Qc 3s Ts Ad 8c 8d Kd Ks Qs 6c 2d 7h 7c 9c 2s 8h Td 6h 6d 8s 9h 2h 3d Ah 2c 6s As 5d Qd 7s Kh 4c 3h 4s Th 5c 3c Jh Jd 9s 4h Qh 7d
check-mcg-32-xsh-rr-16.out:  16bit: 0x0000 0xa790 0x83a2 0x9d79 0x6e9d 0x7b6a 0x3668 0x660f 0x5237 0x91f3
check-mcg-32-xsh-rr-16.out:  Coins: HHHHTHTHTHTTHHHHHTHHTHTTTTTTTTTHTTHTTTHHTHTTTHTHTHTTTHTHTHTTHTHHH
check-mcg-32-xsh-rr-16.out:  Rolls: 4 2 1 4 3 1 6 6 5 2 4 1 4 6 4 2 5 5 3 5 1 4 6 5 1 3 2 6 2 6 4 6 2
check-mcg-32-xsh-rr-16.out:  Cards: As 6c Td 4h 4s 9c 5d 6h Ts 6s 9h Ac Jh Qd Qh 9d Kd 8c 3d 2d Js Ks 5s 7d 5h 5c 7s 2s Jd Qs 2h 9s 7c 2c 3c 8s Jc Kc Th 7h 8d 8h Tc 6d 3s Kh Qc Ad Ah 3h 4c 4d
check-mcg-32-xsh-rs-16.out:  16bit: 0x0000 0xc958 0x0e49 0x3a9a 0xdd16 0x2942 0x6716 0xbc40 0x22f9 0x1ebd
check-mcg-32-xsh-rs-16.out:  Coins: HTTHHTTHHHHTHTTHHTTHHHHHHHHTHTTTHHHTTTHHTTHTHTHTHTTTHTTHHHTHTTTTT
check-mcg-32-xsh-rs-16.out:  Rolls: 3 3 5 6 3 5 4 6 2 5 1 1 2 2 1 6 4 5 5 4 6 3 6 5 6 3 3 4 4 4 3 1 6
check-mcg-32-xsh-rs-16.out:  Cards: 2c Td 2d Jh 4c Js 9s Qs Ts Ac Ad 3h 6d 2s Jc 8s 9c 9d 8h Kh Qh Jd 5d 4h Kd 8d 7s 5h 5c 2h 7c Ah 5s As Tc 3s 6h 3d 3c 7d 6s 7h Th Ks Qd Kc 4s Qc 4d 9h 8c 6c
check-mcg-64-rxs-m-32.out:  32bit: 0xb39d3256 0x48f1ed7d 0xeead926e 0x8f43c0c9 0xd3f8d236 0xed02787c
check-mcg-64-rxs-m-32.out:  Coins: HTTHTHHTTTTHTHTHTTHHTHHHTTTTHTHHTHTHHHHHHHTTHHTHHHTHTHTHHTHHHTTTH
check-mcg-64-rxs-m-32.out:  Rolls: 1 4 3 1 1 6 4 3 5 4 4 1 2 4 4 2 6 1 5 3 2 5 3 3 6 3 6 1 1 5 1 4 5
check-mcg-64-rxs-m-32.out:  Cards: Td Ts 9d 7d 9h 3s Kh 5d Tc 6c Ad 8d 7c Qd Ks 4s 2s 9s Kc Qh 4h 2c 8s 6s 4d Qc Kd 5h 3c 6h 3h Js 3d 2d 2h Qs As Th 8c 4c Jc 5s 5c Jh 7s Ac 7h Ah 8h 9c Jd 6d
check-mcg-64-xsh-rr-32.out:  32bit: 0x00000000 0x21b756ee 0x135e80e8 0xf6025706 0xd2fc74a3 0x157c82ab
check-mcg-64-xsh-rr-32.out:  Coins: TTHTHHHTTTTHHTTTHTHTHTTTHTTTHHTHTHTTHTHTTHTTTHTHTHHTTHTTHHTTTHHHH
check-mcg-64-xsh-rr-32.out:  Rolls: 3 2 3 6 4 4 5 1 1 5 6 6 4 2 6 3 5 2 5 4 4 5 1 5 2 3 3 5 1 5 5 2 4
check-mcg-64-xsh-rr-32.out:  Cards: 9s 7h 3c Td 2c 9c 6d 4s 5c Qd 9h As Kd 6c 6h 4d 2s 2h 8d 8c Jc 9d 7d Qc 8h 5d Tc 7c Js Kc 8s 5s 4h Qh Ac 3d 3s Qs 2d Ts Jh Jd 7s Kh Th Ks Ad 3h 5h 6s 4c Ah
check-mcg-64-xsh-rs-32.out:  32bit: 0x00000000 0x5c400ccc 0x03a8459e 0x9bdb59c5 0xf1c9dcf5 0xaac0af3b
check-mcg-64-xsh-rs-32.out:  Coins: HTHHTHHTTHTHHHTTTTTHHTTTTHTHTHTTHTTHHHHTHHTTTHHTTTTHTTTTHHHTHTHHT
check-mcg-64-xsh-rs-32.out:  Rolls: 1 3 1 4 3 1 4 3 5 1 5 1 6 3 4 6 2 3 3 5 5 2 5 6 5 3 2 4 2 3 1 1 3
check-mcg-64-xsh-rs-32.out:  Cards: 2s 8d 7s 9h Ad Qc Jh 5s 3d 3s 7c Qs Kh Ts 3h Ac 5c 9d 6s 4c 8h 2d Kc 6c 9c 8c 6d 5d As 2h 7h Th Td Js Jd 3c 5h 7d Ah Qd 4d 2c Ks 4s 9s Jc 6h 8s 4h Kd Tc Qh
check-mcg-64-xsl-rr-32.out:  32bit: 0x0000002b 0x3617c502 0x4a8ae596 0xb00afbae 0x8ab2f423 0x51034cd0
check-mcg-64-xsl-rr-32.out:  Coins: TTTTHTTHTTHTTHHTTHTTHHHHTHHHTTTTTTHTTHHTTHHTHTHTTHTTTHTTHTHTTHTTH
check-mcg-64-xsl-rr-32.out:  Rolls: 2 4 3 6 6 5 4 2 3 3 6 4 4 4 3 6 4 5 4 4 3 2 2 6 6 2 5 3 2 1 6 1 1
check-mcg-64-xsl-rr-32.out:  Cards: 3c 2s 2h 8c Jh 5h 9s Qc 8s Ad 4h 7h 8h Td Qs Js 8d 5s 3s Ts 5c Ac 6s 7d 2d Th 3d 6h 4c Qd Kh Jc 7c 9c Tc 9h 6d Qh Ah 4d 7s As Kd 2c 4s 9d 5d 6c 3h Kc Ks Jd
check-oneseq-128-rxs-m-64.out:  64bit: 0x238cceeea3861702 0xfcd963c7707cf608 0x5c952e2c4f97b7a3 0xf23f66d89f351b9f 0xb55975cf3dc499a7 0x574b1955f9435421
check-oneseq-128-rxs-m-64.out:  Coins: HTTHTTHHHTHHHHHTTTHTTTHHTHTTHHHHTTTHTHHHTHTHHTHHHHTTTTHHTHHHHTHTH
check-oneseq-128-rxs-m-64.out:  Rolls: 1 2 4 6 6 1 2 3 3 1 4 5 4 1 6 5 2 6 2 4 2 6 6 3 1 6 5 1 5 5 1 2 1
check-oneseq-128-rxs-m-64.out:  Cards: 2d 9c Ad Jh Js Ts 4c 7h Ah Qd 3d 8d 6c 9s 6d 4h 3h Tc 4d 3c Ac Qc 9d 2s 6s Kc 7s Qh 9h 2c 8h 5h Kh 3s As 5s Qs 5c 6h 8s Kd Th 7d 4s Td 8c 5d 2h Ks Jc Jd 7c
check-oneseq-128-rxs-m-xs-128.out: 128bit: 0x238cceeea3861702c677d3dd5a1ef1fa 0xfcd963c7707cf6085a01cee59a0e770c 0x5c952e2c4f97b7a3b6837eee9f0168ff 0xf23f66d89f351b9f00636575dae31aec 0xb55975cf3dc499a7e95922e3b77881ae 0x574b1955f9435421c029c4b7b65e2aaa
check-oneseq-128-rxs-m-xs-128.out:  Coins: HTTTHHHTHHHHHHTTTHTTTHTTTTTTHHTTTTTTHHHTHTHTHHHHTTTHHHTTHTHTTHHHT
check-oneseq-128-rxs-m-xs-128.out:  Rolls: 2 4 3 4 1 2 6 5 5 3 6 2 6 2 2 1 1 3 3 3 1 1 5 5 6 3 1 2 2 4 5 1 4
check-oneseq-128-rxs-m-xs-128.out:  Cards: Qs 3h 3s 4c 2d 3c 8s 5d 8h 6c Jh 6d As Kd 9s 8d 5c Th 4h 5s 7d 9d Kh 6s Qh Ad 7h 2h 4s Jc Ac 9h 7c Js 6h 4d 5h Jd 2c Qc 7s Qd Ah 8c 9c Tc Kc Ks 2s 3d Td Ts
check-oneseq-128-xsh-rr-64.out:  64bit: 0x61451540c6e348bf 0xc796bfdef9e9e9eb 0x818a6fbb8ae302bd 0xed0bfd3d30bd7a0b 0xe3fca2790bf312ca 0x5c8abb9f257fcfdd
check-oneseq-128-xsh-rr-64.out:  Coins: HHTTHHHHTTHTHTHHTTHTTHHTTHTTTHTTHTHHTHHHHTTHHHTTHTHHHHTTTHHTTHTHH
check-oneseq-128-xsh-rr-64.out:  Rolls: 2 3 5 1 4 2 1 1 1 4 2 4 4 4 2 6 4 5 3 5 2 2 1 2 2 1 5 2 6 2 3 6 5
check-oneseq-128-xsh-rr-64.out:  Cards: Jh 4c Kd 2d 7d 8h Ad 6c Ah Jc 4s Tc 8s 5h Ks 6s Qc Js 5d 2h 3s Td 3c As 9d 3h 7h Qs Qd 4h 2s 4d 5c 9s Jd 5s Ts 6d 7c Kc 3d 9h 2c 7s 6h Ac 8d Qh 8c Kh 9c Th
check-oneseq-128-xsh-rs-64.out:  64bit: 0x540c67b72b5d1c16 0x9ebc796722ebc63c 0xf60629bd535589f8 0x8bed0bfeafdc61a4 0x7e6259a0ac77c858 0xfcfdd5e17dd9d751
check-oneseq-128-xsh-rs-64.out:  Coins: TTHHHTTTHTHHTHHTHHTTTHHHTHHHHHTTTTTTTHTHTHHHHTTHHTTTHTTHHHTTTTTTH
check-oneseq-128-xsh-rs-64.out:  Rolls: 6 6 4 6 5 5 4 6 4 4 1 3 4 3 1 6 5 5 4 5 3 4 4 2 4 5 1 4 4 6 2 3 6
check-oneseq-128-xsh-rs-64.out:  Cards: 5c 8s Qs 3s 9d 6h Ah 6s 3c 5d Th 2d Jh 5h Ks As 6d 4h 5s Td 2s 2h 4c 7s Js Tc Qd 4d Qh Ts 7h 2c 9h 6c 7d 3d 3h Jd 7c Kh Qc 8h 9s Ac Kc Kd 8d 4s 8c Jc 9c Ad
check-oneseq-128-xsl-rr-64.out:  64bit: 0x287472e87ff5705a 0xbbd190b04ed0b545 0xb6cee3580db14880 0xbf5f7d7e4c3d1864 0x734eedbe7e50bbc5 0xa5b6b5f867691c77
check-oneseq-128-xsl-rr-64.out:  Coins: HHTHHHTTHTHHTTTHHHTHHHTTTTTHHTHTTTTHHTHHTTTTTTHHTHTHTTTTTHTHHTTHT
check-oneseq-128-xsl-rr-64.out:  Rolls: 1 2 6 3 6 2 6 5 3 2 3 2 1 5 1 6 1 3 3 5 4 3 1 5 1 4 6 4 1 6 5 5 5
check-oneseq-128-xsl-rr-64.out:  Cards: 9c 5h 7d 7c 4c 8d 7h Qc Kh 2d 3h 2h Qd Ts 3d Kc 9h Jc 6h 6d 8c 4d Qh As Jh 8s Th 5s 2c 9d Ac 4h Kd 5d 9s 6c 3s Ks Js Jd 7s 2s 3c Tc Qs 4s 8h 5c Ah 6s Td Ad
check-oneseq-128-xsl-rr-rr-128.out: 128bit: 0xf7d42ec98a2a818c287472e87ff5705a 0x1e69ebc79672e381bbd190b04ed0b545 0xefb0314dea875a49b6cee3580db14880 0xff56268e0e45f685bf5f7d7e4c3d1864 0x03f0bf312cd0282c734eedbe7e50bbc5 0xfcfdd5e15426494fa5b6b5f867691c77
check-oneseq-128-xsl-rr-rr-128.out:  Coins: HHTHHHTTHTHHTTTHHHTHHHTTTTTHHTHTTTTHHTHHTTTTTTHHTHTHTTTTTHTHHTTHT
check-oneseq-128-xsl-rr-rr-128.out:  Rolls: 3 6 4 3 6 2 4 3 5 4 1 4 3 3 5 6 3 5 5 3 4 5 5 1 3 4 6 2 3 4 3 3 1
check-oneseq-128-xsl-rr-rr-128.out:  Cards: 3s Jd Qd Ts 8s 4c Kh 4s Ac Ad 9c Ah 5d 7d 7h Jc Ks Qh 6h Th 6d 9d 6c 9h 7s 2d Qs Kc 3h Jh Qc 4h As 2h Tc Td 2c 8h Js 7c 5s 6s 5h 3c 2s 9s Kd 5c 3d 8c 8d 4d
check-oneseq-16-rxs-m-8.out:   8bit: 0x7f 0x7f 0x54 0xe8 0x94 0xba 0xb7 0x21 0x39 0xb0 0x71 0xa9 0x05 0x81
check-oneseq-16-rxs-m-8.out:  Coins: HTTHTTHHHHHTHTTTHTHHTHHHTTHTHTTHTHHHTHTTTHHTHTTTTHTHHHHHTHHTHHTTH
check-oneseq-16-rxs-m-8.out:  Rolls: 6 3 4 2 5 1 1 6 3 2 3 3 5 4 5 2 2 4 1 3 6 4 3 6 3 4 1 5 2 3 4 4 2
check-oneseq-16-rxs-m-8.out:  Cards: 4c Jc Qs Th 8c 8s 9d Qc Td As Jd 9h Ac Ah 2s 8d Qh 5d Ad Tc 6c 7c Ks 2h 3c 6s 2c 4s 6d 3h Jh Ts Kc 2d 5c 4d 5s Js 9s 9c 6h 7d 4h Kd 3d 8h Kh Qd 3s 5h 7s 7h
check-oneseq-16-rxs-m-xs-16.out:  16bit: 0x7f90 0x7f82 0x54f7 0xe8c8 0x9444 0xba1a 0xb7fb 0x2167 0x39dd 0xb0f2
check-oneseq-16-rxs-m-xs-16.out:  Coins: TTTHTHHHTTTHHHHHHTHHHTTTTTHHHHTHHTHHTTTTHTHTTHHHHTHTHTHTTHTHTHHTT
check-oneseq-16-rxs-m-xs-16.out:  Rolls: 6 5 2 4 1 1 1 6 4 2 5 2 3 5 6 5 1 2 5 2 2 5 3 2 5 4 4 3 5 4 3 5 3
check-oneseq-16-rxs-m-xs-16.out:  Cards: 2c Th Qs Qc Ad 3d 5s 6h 9c 5d Jc 9d Tc 2h 2d 8d 6c 3s Ah 5h Jh 4h As 4c Kd 5c Jd Kc Ts 3c Qd 9s 7c 8c 2s 8h Js Qh 6d Ac 4d Kh 6s 7h 8s 4s 7d 9h Td 7s 3h Ks
check-oneseq-16-xsh-rr-8.out:   8bit: 0x51 0xb3 0xa8 0xcb 0x37 0xdf 0x72 0xc7 0x84 0x09 0x5c 0x35 0xf2 0xf8
check-oneseq-16-xsh-rr-8.out:  Coins: HHTTHHTHHTTTTHHTHHTTHTTHHTHTHHTHHTTTHTHHHHHHTHTTHTTTTHTHTTTTTHTHT
check-oneseq-16-xsh-rr-8.out:  Rolls: 1 4 4 3 6 3 6 5 3 2 5 6 1 2 3 6 5 3 6 4 5 6 6 6 3 3 1 3 1 4 4 3 1
check-oneseq-16-xsh-rr-8.out:  Cards: 6d 8c As Jd 7s 7d Ah Qc Kh 2h 5d 3h 2d 7h 7c 8d 8h Ad 9c Kc 3d 9s 6c 5h Qs Qh Td 5s 4d Tc 3c Qd 4h Kd 4s 4c Th Jh 8s Ks Ac 9h Jc Js 2s 5c 2c 9d 6s Ts 6h 3s
check-oneseq-16-xsh-rs-8.out:   8bit: 0x4d 0xbb 0xa6 0x61 0x47 0x66 0xb5 0x65 0x63 0x03 0xd9 0x8f 0x42 0x01
check-oneseq-16-xsh-rs-8.out:  Coins: HTHTTHTTHHTTHHTHTTHHTTHTTHTTHTTHHTTHHHTTHTHHTTHHTTTTHTTHHTTHTTHTH
check-oneseq-16-xsh-rs-8.out:  Rolls: 2 4 6 2 3 1 5 4 6 1 6 1 1 5 6 2 6 2 6 3 3 1 5 2 1 3 2 1 4 6 2 6 1
check-oneseq-16-xsh-rs-8.out:  Cards: 6s Ad 2h 8s 4h Qc Tc 3c Kc Kd Qd 6c Ks 3d 8h Th Js Qs Jh 9h 9d 7c As Td 4c Qh Kh 7d Jc 7h 4d 5c 5h 4s 8d 3s 7s 6d Ac 9c 9s 6h 5d 2s Ts Ah 2d 8c 2c Jd 3h 5s
check-oneseq-32-rxs-m-16.out:  16bit: 0x256b 0xa5ef 0x170b 0x334a 0x3de5 0x9b47 0xd3d0 0xa661 0x201c 0xf034
check-oneseq-32-rxs-m-16.out:  Coins: TTHHHHHTTHHHHHHTTTTTTHTHHTHHTTHHTHTHTTTHTHTTTTHHHHHHHHTHTHTTHHHTH
check-oneseq-32-rxs-m-16.out:  Rolls: 3 1 6 1 5 3 2 6 6 1 5 5 3 2 4 5 4 1 6 1 1 6 5 6 1 4 4 2 2 4 6 3 2
check-oneseq-32-rxs-m-16.out:  Cards: 4d Tc 2c 8s 8h Ts 9c Ks 3s 4s 2h 9h 9d 6d 8c Jc 8d 3h 7d 4c 3d 6h 5h Ah Td Jh 3c Th 5s 4h 2d Ad Kd Kc 5d As Qs 5c 7c 9s 2s 7h Qh 7s 6c Jd 6s Js Qc Qd Kh Ac
check-oneseq-32-rxs-m-xs-32.out:  32bit: 0x256b5357 0xa5efad32 0x170b7830 0x334a5b22 0x3de5c680 0x9b47b7b3
check-oneseq-32-rxs-m-xs-32.out:  Coins: HTHTHTHHHTTHTTTTTTHHTTTHHTTTHHTTHHTHTTHHTHHTTTTTHTTTHHHHHHHHTTTTT
check-oneseq-32-rxs-m-xs-32.out:  Rolls: 5 5 5 1 5 6 5 1 3 4 5 3 4 5 4 5 2 5 6 4 5 4 4 5 5 6 4 3 6 3 5 4 5
check-oneseq-32-rxs-m-xs-32.out:  Cards: 3c 5c Kc 6s Qh 7s Jh 4d 3s 5d 9h Th Qs 7h 4c 7c Qd 2d 3h 5h 2h 6c 6d Js Jd 9d 8s 9s 9c Qc Kh 8d 8c 2s Tc 4s Ac 2c Jc Ks As Ah 6h Ad Ts 7d 3d 8h 5s Kd 4h Td
check-oneseq-32-xsh-rr-16.out:  16bit: 0xfc39 0x0fa4 0x1e71 0xe52b 0x39f1 0xc552 0x1023 0xcb48 0x91cd 0xfdd6
check-oneseq-32-xsh-rr-16.out:  Coins: THHTHTHTTTHTHTTTTHTHTHHHHTHTHTHHHTTTHTTTHTTTTTHTTHHHHTTHTTTTTHHTH
check-oneseq-32-xsh-rr-16.out:  Rolls: 2 6 2 5 3 3 5 6 1 4 6 3 4 4 3 5 1 4 3 1 2 5 2 5 4 1 1 5 2 6 3 6 1
check-oneseq-32-xsh-rr-16.out:  Cards: 5h 9c 9h Qs 2h Ad 9s 6c 4c 4s 8d Qc 9d As 6h Ks 7c Qh Ah 6s 2s Js Jh Tc 3d Th 6d 3c 5s Kd Td 4d 7d 7h 5d Jc 2d 8c 3s Kh 2c 4h Kc 7s Qd Ts 8h Ac 5c 3h Jd 8s
check-oneseq-32-xsh-rs-16.out:  16bit: 0xb845 0xfb21 0x39a6 0xa4a1 0x4974 0x15ed 0x0ce2 0x68ee 0x23f6 0xaf43
check-oneseq-32-xsh-rs-16.out:  Coins: HTHTHTTTHTHHTHTTTHTTTHHHTTHTHHTHTHHTTTHHTTHHTTTTTTTHHHTTHHHTTHHTT
check-oneseq-32-xsh-rs-16.out:  Rolls: 2 3 4 6 1 2 2 6 6 4 4 4 6 4 6 3 5 2 5 2 4 4 1 5 6 5 5 4 3 2 1 1 3
check-oneseq-32-xsh-rs-16.out:  Cards: Tc 6s Td Jd Kd 5s 6h Ts Jh 7h Qh 2c 3c 7c 3s 2s Qc 9d Qd 8h 7d 9s 6c 9c Th 4s As Kc 8c 6d Qs 3h Ks Kh 4c 2h 8d 3d Ac 7s 5c 8s Ad 4d 5h 9h 2d 5d 4h Jc Ah Js
check-oneseq-64-rxs-m-32.out:  32bit: 0x27a53829 0xdf28458e 0x2756dc55 0xa1032555 0x40a0fccb 0x5c2047cf
check-oneseq-64-rxs-m-32.out:  Coins: THTTTTHHHHTHTHHHHTTHHTHHTTTTTTHHTHTHTTHHHTTTTTTHTHTHTHHHHHTTHTHHH
check-oneseq-64-rxs-m-32.out:  Rolls: 5 3 3 5 4 3 4 6 5 4 6 4 4 5 4 4 2 3 5 3 2 1 6 6 1 6 4 1 2 6 5 5 6
check-oneseq-64-rxs-m-32.out:  Cards: 6c Ah 2s 6h Kc 4d 5s Th 4c Js 5h 3d Qs 7h Ts Kd 8c 7c 5d 4h Td Jc 3s Qh 9d As 2c 3c 6d Qd Jd 8h 9s 6s Kh Ad 8s Qc 5c 3h 7d Jh 7s Tc Ac Ks 2d 8d 2h 9h 9c 4s
check-oneseq-64-rxs-m-xs-64.out:  64bit: 0x27a53829edf003a9 0xdf28458e5c04c31c 0x2756dc550bc36037 0xa10325553eb09ee9 0x40a0fccb8d9df09f 0x5c2047cfefb5e9ca
check-oneseq-64-rxs-m-xs-64.out:  Coins: TTHHHTHHTTHHHTHTTHHTHHTTHHHTTHTTTTHHHTTHTHHHHTHTTTTTTHHHTTTHTTTTT
check-oneseq-64-rxs-m-xs-64.out:  Rolls: 6 4 3 6 3 4 6 6 2 3 5 6 1 4 3 6 4 1 5 2 3 4 2 5 4 5 3 6 6 4 4 2 2
check-oneseq-64-rxs-m-xs-64.out:  Cards: 3d Ah 7d Kh Td Th 8h 5d Ac 7c Ts As 7s 2s 2c 4h 8s Kc 3c 5h 6s 9c 4s Js Kd Qc 3h 2d Jh 6d 7h 3s 9d Tc 5s 9s 9h 4d Ks Jd Qd Ad Qs Jc 8d 4c 6c Qh 8c 5c 6h 2h
check-oneseq-64-xsh-rr-32.out:  32bit: 0xc2f57bd6 0x6b07c4a9 0x72b7b29b 0x44215383 0xf5af5ead 0x68beb632
check-oneseq-64-xsh-rr-32.out:  Coins: THTHHHTTHHTTHTTHTHHHTHTTTHTTHTTHTTTHHTTTTTHHTTTHTTHTHHTHHHTTHTTTH
check-oneseq-64-xsh-rr-32.out:  Rolls: 4 1 3 3 6 6 5 1 3 4 4 3 2 2 5 4 1 3 3 3 1 4 6 4 6 6 1 6 1 2 3 6 6
check-oneseq-64-xsh-rr-32.out:  Cards: 2d 5c 3h 6d Js 9c 4h Ts Qs 5d Ks 5h Ad Ac Qh Th Jd Kc Tc 7s Ah Kd 7h 3c 4d 8s 2c 3d Kh 8h Jc 6h 4c 8d Qc 7c Td 2s 3s 4s 7d Qd Jh As 6c 8c 5s 2h 6s 9d 9s 9h
check-oneseq-64-xsh-rs-32.out:  32bit: 0xdebff77f 0x54b00b9c 0xded17109 0x383d10fa 0xb7d5e650 0xd8c19fa9
check-oneseq-64-xsh-rs-32.out:  Coins: HTTHHHTTTTTTHHHTTTTHTTTTTTTTTTTTHTTTTTTHTHTHHHTHTHTTTHTTHTHHHTTHT
check-oneseq-64-xsh-rs-32.out:  Rolls: 3 6 2 3 6 5 3 3 4 6 3 2 3 5 4 2 5 3 2 4 3 2 2 4 2 1 5 5 3 4 6 6 5
check-oneseq-64-xsh-rs-32.out:  Cards: 9c 5s 6c 2c 3c 6d Ac Kd 8h Ad Jd Td As Qs 5c 2h 4h 5d Qh 4d Ts Tc 5h 7s 7d Th 2s 6s 9d 2d 9h Kc Js Jh Ah 4c Jc 3d 9s 8c 8d 7c 6h 4s Qd Qc 3h 7h 3s Kh Ks 8s
check-oneseq-64-xsl-rr-32.out:  32bit: 0xedc5208e 0x4117cb95 0x2c3158e6 0x5ed1be83 0x18a71004 0x6b32511a
check-oneseq-64-xsl-rr-32.out:  Coins: THHTTTHTHTTTTHHHHTHTHHHTTHHTHTTTTHTTTTTTTHTTHHHTHTTHTTTTHTTTTHTTH
check-oneseq-64-xsl-rr-32.out:  Rolls: 2 2 4 3 3 2 6 1 2 3 4 1 2 6 2 6 2 5 4 6 1 6 5 4 2 6 1 3 2 2 4 5 4
check-oneseq-64-xsl-rr-32.out:  Cards: 9c 4h Kc 5d 5s 7d 7h 8h Ac 6d 2d 9d 9h 4c 8d Th Qc Jc Qd 7c As 6c 2h Jd 6h 4d 5c Ah Ks Qh 5h 4s Js 8s 6s Qs 9s Kh 3h Kd Tc 8c 2c 7s 3s 2s Jh 3c Td 3d Ts Ad
check-oneseq-64-xsl-rr-rr-64.out:  64bit: 0xf6025debedc5208e 0x2c15b5d54117cb95 0xb4d5bda12c3158e6 0x139c1ec65ed1be83 0x8db7d5d018a71004 0xd8c114e26b32511a
check-oneseq-64-xsl-rr-rr-64.out:  Coins: THHTTTHTHTTTTHHHHTHTHHHTTHHTHTTTTHTTTTTTTHTTHHHTHTTHTTTTHTTTTHTTH
check-oneseq-64-xsl-rr-rr-64.out:  Rolls: 2 2 6 5 1 6 2 5 6 3 2 1 6 6 4 2 6 5 4 6 5 4 1 6 2 4 5 1 6 4 2 5 2
check-oneseq-64-xsl-rr-rr-64.out:  Cards: Jh Ks Tc 9h 2c 8s 5c Jd 9c 3h 7h 7s Qd 8h Js Qh 6s Qc Ah 6d Ts 9d Kh 9s 8d 3d 6h 5h 4d 7c 5s Th 4h 4c Ac 6c Td 5d 4s 2s Kc 8c Ad 3s Qs As Kd 3c Jc 2d 2h 7d
check-oneseq-8-rxs-m-xs-8.out:   8bit: 0x2e 0x44 0x2f 0x91 0x50 0x84 0xcb 0x60 0x4b 0xe5 0x5f 0x97 0x0f 0x58
check-oneseq-8-rxs-m-xs-8.out:  Coins: THTTTHHTHHTHTTTHTHHTTTTHTTHHHHHTHHHTTTHHHTHHHTTTTTTHHHTTHTHTTTHHH
check-oneseq-8-rxs-m-xs-8.out:  Rolls: 6 5 2 5 6 5 6 3 5 5 6 6 4 1 2 4 5 6 6 3 1 3 5 5 3 5 5 2 3 5 4 1 2
check-oneseq-8-rxs-m-xs-8.out:  Cards: Qs 8s 6s 3s 3h Qc Ad 2h 6c 8d Kc Kd 9s Jc Jh 3d 9h 4s Ts Ks 8h 9d Jd 5s 3c 7c 2c Qh 7h Tc 2s 4h 8c Ac Th 4d 5c Ah 6h 5d 9c Kh 7s 2d Qd As 5h Td Js 7d 4c 6d
check-setseq-128-rxs-m-64.out:  64bit: 0xa8a720f5a159081c 0xb29c5717724f8aa7 0x218f48a6286a8e29 0x25b87162fd532772 0xa4702c543aceda62 0x58a055e64cc295b3
check-setseq-128-rxs-m-64.out:  Coins: HHHTTHTHHTTHTHTHHTHTHTHHHTTHHHHHHTTHTTHTHTTHHHTHHTHTTHTHTHHHHHTHT
check-setseq-128-rxs-m-64.out:  Rolls: 5 3 6 5 2 6 6 6 4 6 4 6 6 6 2 2 5 4 3 5 2 4 6 1 6 2 6 6 3 4 3 4 4
check-setseq-128-rxs-m-64.out:  Cards: 3s 6d 2c 7c Kd Ks 3d Jh Jc 4d Kh Qc Js As 4s 8d 2h Qd 8c 7d 4c Ac 5h Jd Qs 6h 9h 2s Tc Ts 3c 8s 9s 9d 6c Th 5d 7s 6s 7h 4h Ah Qh Td 3h 5c Ad 5s Kc 8h 2d 9c
check-setseq-128-rxs-m-xs-128.out: 128bit: 0xa8a720f5a159081c618b176cf5862246 0xb29c5717724f8aa750aeecd6858ffe10 0x218f48a6286a8e2975388c4e976edbc2 0x25b87162fd532772c2335c62014d1b85 0xa4702c543aceda62a39c1a78d96bc49c 0x58a055e64cc295b3d1a2fbeb433921cf
check-setseq-128-rxs-m-xs-128.out:  Coins: THTTTHTHTTHHTHTHTTHTTTTHTTTHHHTHTTTHHTHHHTHHHTHTTHTTTTHTTTTHTHTTH
check-setseq-128-rxs-m-xs-128.out:  Rolls: 6 6 5 6 5 5 5 3 5 3 5 4 2 3 5 5 3 2 4 1 2 1 5 3 5 1 3 3 2 2 2 3 4
check-setseq-128-rxs-m-xs-128.out:  Cards: 2d 7d 7c 8c 3s 8s Jh Ah Ac Kc 4c 5c 2s 6c 3h Ks 5s 3d 4h 9c 8d Kd Qs 2c Jc Qd Qc Ts Qh 7s 9h 6d Td Tc 7h 6s 4s 6h Th 5h Kh 9s 9d 2h Ad Jd 3c Js As 5d 4d 8h
check-setseq-128-xsh-rr-64.out:  64bit: 0x42bc197d32f2393a 0x920f68ff6341b1cb 0x265b380ff57b5fc7 0xb49cee1fecd7defd 0xf30b09986ecda74c 0x32cef26465ce0494
check-setseq-128-xsh-rr-64.out:  Coins: HHHHTHTTHHHHTTHTHHHTTTTHTTHHHTHHTTHTTTHHTTHHHTTHHTTTHTTHTHTTHHTHH
check-setseq-128-xsh-rr-64.out:  Rolls: 4 1 6 2 5 6 4 4 2 6 2 6 1 6 6 2 5 6 2 5 6 1 5 1 2 3 1 2 1 1 2 2 4
check-setseq-128-xsh-rr-64.out:  Cards: 5h 5s 6d 4h 6h Ks 8h Jd As 9h 3h 2h 3c Ah Tc Th 2d Kc 8s Qd Ac Js Jh 3d Ad Qh 7c 9d 4c 4s 7s 2c Qs 8c 9s 5c 7d 4d 2s 3s Ts 6c 8d Td Kh Kd Qc 6s Jc 9c 7h 5d
check-setseq-128-xsh-rs-64.out:  64bit: 0x197d3aaded96c16d 0x41b1cb1eeb36f03b 0xbf8e4cba6bf9d2a8 0x9dc3ff4a5a1466a4 0xbb369c8e477d9305 0x65ce050922fb8b1c
check-setseq-128-xsh-rs-64.out:  Coins: HTTTHTHHHTTHTTTHTHTTHTHTTTTTTTTHTHTTHTHHTHTTTHTTHTHHTTTTTHTHHTTHT
check-setseq-128-xsh-rs-64.out:  Rolls: 5 4 1 2 5 4 6 2 3 4 5 6 3 4 6 1 4 4 5 6 2 6 4 4 6 5 3 4 3 4 6 5 1
check-setseq-128-xsh-rs-64.out:  Cards: Ac 3h Tc 7d 5c Td 5d Jh 3s 3c Jc Ts 6s 4c Qh 2c 2s 7s Th 8h Ah Jd As Ks Js 3d Qc 8d 9d Ad 7c 6c 5h 8c 9s Kd 9c Qs Kc Qd 6h 4h 7h 5s 4s 8s 6d 4d Kh 2h 9h 2d
check-setseq-128-xsl-rr-64.out:  64bit: 0x86b1da1d72062b68 0x1304aa46c9853d39 0xa3670e9e0dd50358 0xf9090e529a7dae00 0xc85b9fd837996f2c 0x606121f8e3919196
check-setseq-128-xsl-rr-64.out:  Coins: TTTHHHTTTHHHTTTTHHTTHHTHTHTTHHTHTTTTHHTTTHTHHTHTTTTHHTTTHHHTTTHTT
check-setseq-128-xsl-rr-64.out:  Rolls: 6 4 1 5 1 5 5 3 6 3 4 6 2 3 6 5 5 5 1 5 3 6 2 6 1 4 4 3 5 2 6 3 2
check-setseq-128-xsl-rr-64.out:  Cards: 3d 7d 3h Qd 9d 8c Ts Ad 9s 6c Jh Ac 5s 4c 2c 7s Kh Kd 7h Qh 6d Qc 8d Qs 6s Js 4d Kc 9h 3c 2h Td 5d 5h 9c 4s 5c 7c 3s 4h As Th 6h Jc 2s Jd Tc Ah 2d Ks 8h 8s
check-setseq-128-xsl-rr-rr-128.out: 128bit: 0x5f4ea96e8510af0686b1da1d72062b68 0x341b1cb1e675ec461304aa46c9853d39 0xcfdc46c17f1c9974a3670e9e0dd50358 0x02d273b87fe9110cf9090e529a7dae00 0x9b4e47fda576f0ddc85b9fd837996f2c 0x17cee59c8cb9c0a1606121f8e3919196
check-setseq-128-xsl-rr-rr-128.out:  Coins: TTTHHHTTTHHHTTTTHHTTHHTHTHTTHHTHTTTTHHTTTHTHHTHTTTTHHTTTHHHTTTHTT
check-setseq-128-xsl-rr-rr-128.out:  Rolls: 4 4 1 5 3 1 1 3 4 5 4 6 2 5 2 5 3 1 3 3 1 4 6 2 5 4 4 3 3 6 6 3 2
check-setseq-128-xsl-rr-rr-128.out:  Cards: As 2s 8h Tc 3c 5h Ac 5c 9d 8d Td Ts 4c 7d 9c Jh 3d Th 8s 5s 2h 6d 7h 3h Qd 7c Qh Ks Js Kh 6s 3s Qc Ah 6h 9s Ad Kc 6c Kd 5d Jd 2d Jc 2c 4d Qs 9h 7s 8c 4h 4s
check-setseq-16-rxs-m-8.out:   8bit: 0x9b 0x59 0x96 0xd0 0x4e 0xde 0x03 0x1f 0xee 0xf6 0x4a 0xed 0x9b 0x8a
check-setseq-16-rxs-m-8.out:  Coins: HHTHHTTTHHTHHHTTHTHHHTTTTTHTTTHHHTTTTHTTTTTTTHTHTTHHTTHTHHHHHHTHT
check-setseq-16-rxs-m-8.out:  Rolls: 5 5 3 3 4 4 3 4 4 4 4 5 1 2 5 1 2 4 6 4 3 6 5 6 3 2 6 2 3 1 6 5 1
check-setseq-16-rxs-m-8.out:  Cards: 7d 9d 6h 3h Qc 6d Ad Jh 2c 5d As 4c 5s 8h 5h 7h Kc Qd Td Qs 5c Jc Ac 3c 7c 4d 3d 9s 2s Ts 8c Ks 3s Ah 4h Js 2d Th Kh 4s 6s 9h Tc Qh 8s 6c 8d Jd 2h Kd 9c 7s
check-setseq-16-rxs-m-xs-16.out:  16bit: 0x9bec 0x5957 0x960e 0xd08d 0x4e05 0xde00 0x03f7 0x1fa6 0xee22 0xf6fa
check-setseq-16-rxs-m-xs-16.out:  Coins: HTTHTTTHTTHTTHHHTTHHTTTTHTHHTHHTHTTHHHTTHTTHTHHTTTHTHTTHTHTHHTHTT
check-setseq-16-rxs-m-xs-16.out:  Rolls: 3 2 1 6 1 4 1 3 6 4 4 4 3 1 6 4 3 6 3 5 2 6 5 2 4 4 2 4 1 2 1 4 4
check-setseq-16-rxs-m-xs-16.out:  Cards: 3s 7c Td 2h Js 5s 2s 6h 7d As Ad 9c Kc Ah Jc Ks Qc 8s 6s 9h 8d Qh 3h Kd Kh 5h 7s 3c 4h 9s 8h 7h 9d 6d Tc Ts 3d Jd Qs 5c Qd 5d Jh 2c 4s 2d 6c 4c Th Ac 4d 8c
check-setseq-16-xsh-rr-8.out:   8bit: 0xf5 0x8a 0x7f 0xcb 0xed 0xfe 0xad 0xe7 0xc7 0x2f 0x13 0xcc 0x15 0xcd
check-setseq-16-xsh-rr-8.out:  Coins: HHTHTHHTTHTHHTHHHHHTTTTHHHHTHTTTHTTTHHTHHTTHTHTTHTHHHHTTTTTTHTTHH
check-setseq-16-xsh-rr-8.out:  Rolls: 2 3 1 4 6 3 6 5 3 3 5 1 3 6 6 4 4 2 5 5 1 1 4 2 1 3 2 4 6 2 5 1 6
check-setseq-16-xsh-rr-8.out:  Cards: 2c Qd As 9s 4d Kc Jh 5d Tc Qs 7c Ad 6d 3s Kd 3h 6c Ks 4h Kh 6s Td Jc 2d 9d 5h Ah 8c Ac 3d Qh 9h Ts 2s 4s 7h 9c 3c 2h 5c 5s 8d Qc 6h 8h Jd 7s 8s 4c Js 7d Th
check-setseq-16-xsh-rs-8.out:   8bit: 0x74 0x4d 0xc7 0x64 0xaf 0xc0 0x2a 0x0b 0x56 0x58 0x06 0xa9 0xdc 0x53
check-setseq-16-xsh-rs-8.out:  Coins: HHHHTHHHHTTTTTTHHTHHTHTHTHTHHTTHHHHHTTTHTHTHTHHHHHTTTTHHHTTTHTHTT
check-setseq-16-xsh-rs-8.out:  Rolls: 3 2 5 6 6 2 2 2 1 3 5 1 4 2 5 6 4 2 3 1 2 6 3 3 3 1 3 6 6 3 4 1 2
check-setseq-16-xsh-rs-8.out:  Cards: Jd 6s 3d Qh 2c 7c Ts Kc 9d 5s Td Th Jc 4c 7h 8h 2h 9h Ad Ac Js 4h Qs Ks Kh 5d 9c 6c 4d 8c 7s 2s Qc 4s 5h 3c As 9s 3h Qd 2d 6h 5c Kd Ah 8s 3s 8d Tc 6d Jh 7d
check-setseq-32-rxs-m-16.out:  16bit: 0xf84b 0xdc1e 0x74fb 0xb3bb 0x9cf6 0x2d2f 0xc110 0x6172 0x810d 0xbca2
check-setseq-32-rxs-m-16.out:  Coins: TTTHTTHHTHTHHHHHHHTTHTHHHHHTTTHHTTTTHTHHTHHHTTTHHTTHTTTTTTTTHHHHH
check-setseq-32-rxs-m-16.out:  Rolls: 3 3 6 3 4 1 1 1 1 4 5 1 4 5 4 5 5 2 6 6 1 6 6 3 1 1 5 4 4 6 4 2 3
check-setseq-32-rxs-m-16.out:  Cards: Jh Js 5s Kc Ad 3d 6h 2s Qh Td 7d 8c Qs 8d 6c 5c 9h Qd Jd 5h 7c Th Ac 9c 4d 4s 3h Kd 2d 5d 7h 4h Ts 3s Ah 8h Qc Tc 2h 3c 6s Ks 4c 6d 2c Kh 9s As 7s 8s Jc 9d
check-setseq-32-rxs-m-xs-32.out:  32bit: 0xf84b622d 0xdc1e5bb4 0x74fb8ac1 0xb3bbf8de 0x9cf62074 0x2d2f5e33
check-setseq-32-rxs-m-xs-32.out:  Coins: HTHHHHTTTTTHHHHHTTTTTHHTTTHTTHTHTTHTTTTTHTTTHHTHTTHTTHTTTTHTTHHHT
check-setseq-32-rxs-m-xs-32.out:  Rolls: 6 1 5 3 2 4 4 6 5 1 1 5 4 2 6 4 6 5 1 6 5 2 5 4 6 2 6 3 5 1 6 4 3
check-setseq-32-rxs-m-xs-32.out:  Cards: Kd 8h Td 9d As 4h 7s 5s 2s Js Qd 5h Ac 3d 8d 2d 3h 8s 7h Th 4d 9s Qc 3s Kc 6d 7c 9h 4c 8c Kh Jh Jc 4s 3c Ah Tc 6s 9c 7d 5c Ks 6c Qs Ad 6h Ts Jd 2h 2c 5d Qh
check-setseq-32-xsh-rr-16.out:  16bit: 0x0a76 0x61e2 0x8b45 0xdacf 0x2b23 0x9447 0xb23a 0x284b 0xfcee 0x7d32
check-setseq-32-xsh-rr-16.out:  Coins: TTHTHHTTTHHHTTTTTTHTHHTTHTHTTHHTTTTHHTTHHTTTHTHTTHHHHHTHTHHHHHTTH
check-setseq-32-xsh-rr-16.out:  Rolls: 4 4 5 3 2 3 4 4 3 4 4 1 4 1 3 3 3 2 4 1 6 1 5 3 2 5 2 1 1 3 3 4 2
check-setseq-32-xsh-rr-16.out:  Cards: 4c 6h 5s Td 3c Qs 3h 5d 8h Ac Ks 7c Jd 8c Js Qc 9c 5c 4d Jh 9h 9s Ad 4s 6d Ah 7h Qh Qd 8s Tc 3s 2h 2d 5h 7s 6c 9d As Kc 4h 7d Kd 2c Th 3d 2s 8d Kh 6s Jc Ts
check-setseq-32-xsh-rs-16.out:  16bit: 0xa6dd 0x8854 0x5bb1 0xade3 0x6590 0x8921 0x8fde 0x0886 0xef5a 0xcf2c
check-setseq-32-xsh-rs-16.out:  Coins: TTTTTHTHTHHTHTHTHHHHTHTTHTHTTTTTHTHHTTTHTHHHHHHTTTHHHTHTHHHTHTHTH
check-setseq-32-xsh-rs-16.out:  Rolls: 2 5 3 2 5 5 6 1 5 5 6 2 6 1 4 3 6 1 3 6 4 2 2 1 2 2 2 3 4 1 1 1 3
check-setseq-32-xsh-rs-16.out:  Cards: 3c 2s Jc Ah 6s Kd Jh Td Qc 9c 7h 4h Kh Qh 2c Qd Js 2h Kc 6c 8d 7s 8s Ac 6h 6d 3h Th Jd 5d 8h 5h 8c 7c Ad As 7d Ks 2d 4s 9s 9h 5c Tc 9d 4c Qs 3d Ts 3s 4d 5s
check-setseq-64-rxs-m-32.out:  32bit: 0xe1cbc180 0x6573bce7 0xc744f074 0x9e9f98cc 0xde693821 0x263cc2cd
check-setseq-64-rxs-m-32.out:  Coins: HTTHHHHHHHTHHTTHHHHHTHTTTHTTTHHHTTHTHTHTTTTTHTTTTHTTHHTHTHTHTHTHT
check-setseq-64-rxs-m-32.out:  Rolls: 3 1 3 1 6 3 2 2 3 5 6 5 6 4 2 3 2 2 5 6 6 6 2 4 5 1 4 4 4 5 1 4 3
check-setseq-64-rxs-m-32.out:  Cards: Qs 7c 6s 3d 9h 2c 4s 2d 3s Kc 6h 4d 5d 6d Ac 7s 7h 2h 3c 9s Ts Jc Qd 8c 5h Jh Th 7d 5c Kd 4c Td 2s 8h Qh 8d Ad 6c Tc Js 8s Qc 9d Ks Ah 5s As 3h 4h Kh 9c Jd
check-setseq-64-rxs-m-xs-64.out:  64bit: 0xe1cbc180b69606bb 0x6573bce7abaee684 0xc744f07442006076 0x9e9f98ccbd60b8fc 0xde693821ee9629ae 0x263cc2cdc66ebc25
check-setseq-64-rxs-m-xs-64.out:  Coins: THHHHTHHHHHTHTTHHHHTHHHTTTHTTHHTHTHTHHHHHHTHHTTHHTHHTHHTHHTTTHTTT
check-setseq-64-rxs-m-xs-64.out:  Rolls: 3 1 1 6 1 3 1 1 3 1 1 4 1 5 3 1 2 6 1 1 6 1 4 6 5 2 6 2 2 3 6 3 5
check-setseq-64-rxs-m-xs-64.out:  Cards: Qd Qc Th 5s 6h 5c 2c 3s Ts Jh 9s Kh 4h Td Ad Ks 6c Ah 7h 8s 6s 8d 2d Jc 2h 4s 9d Kc Qs 4d 3c 2s Kd 4c Tc 9c 7d 9h 8c 5h As Qh 8h 6d 7c Js 3h 5d Ac Jd 7s 3d
check-setseq-64-xsh-rr-32.out:  32bit: 0xa15c02b7 0x7b47f409 0xba1d3330 0x83d2f293 0xbfa4784b 0xcbed606e
check-setseq-64-xsh-rr-32.out:  Coins: HHTTTHTHHHTHTTTHHHHHTTTHHHTHTHTHTTHTTTHHHHHHTTTTHHTTTTTHTTTTTTTHT
check-setseq-64-xsh-rr-32.out:  Rolls: 3 4 1 1 2 2 3 2 4 3 2 4 3 3 5 2 3 1 3 1 5 1 4 1 5 6 4 6 6 2 6 3 3
check-setseq-64-xsh-rr-32.out:  Cards: Qd Ks 6d 3s 3d 4c 3h Td Kc 5c Jh Kd Jd As 4s 4h Ad Th Ac Jc 7s Qs 2s 7h Kh 2d 6c Ah 4d Qh 9h 6s 5s 2c 9c Ts 8d 9s 3c 8c Js 5d 2h 6h 7d 8s 9d 5h 8h Qc 7c Tc
check-setseq-64-xsh-rs-32.out:  32bit: 0x5c1b65c0 0x8ffceb31 0xcccad075 0xb83cdfc6 0x5dfce9ca 0xc0d524ec
check-setseq-64-xsh-rs-32.out:  Coins: THHHTTHHHTTTTHHTTTTHTHTHTTTHTTTHHTTTHHTTHHTHTTHTTTTTHTTHTTHTTHTTH
check-setseq-64-xsh-rs-32.out:  Rolls: 1 3 4 6 5 5 4 1 4 5 5 6 2 1 2 4 3 3 6 5 6 6 5 3 3 4 6 2 5 4 1 3 5
check-setseq-64-xsh-rs-32.out:  Cards: Qh 8d 8h 6c 7h As Kc Qc 2c 9s 3d 6d 5h 5s Qs Ts Th 8s 4c 2h 9c Kh 9h Qd Ac 3s 2d 3c 3h 7s 4d Ad 4h 9d 2s 4s 7d 6h 5d 8c Tc 6s 7c Jd Jc 5c Td Ks Kd Ah Js Jh
check-setseq-64-xsl-rr-32.out:  32bit: 0x068f20a8 0xed610a2e 0x3911c946 0xd94c9c1c 0x0d4b401a 0x92ee3d83
check-setseq-64-xsl-rr-32.out:  Coins: TTTTHHHTTTHHTTTTHHTTHTTTHHTHTTHTTHTHHTHTTHHHTHTHHTHTTTTTHTHHHHHHT
check-setseq-64-xsl-rr-32.out:  Rolls: 5 6 1 1 2 4 6 5 1 2 3 1 4 5 6 4 4 1 1 1 4 1 5 3 4 5 4 6 5 5 1 3 5
check-setseq-64-xsl-rr-32.out:  Cards: Ac Js 5d 6d 9c Kh 2c 5h Jc 8s Th Qc 6c 8h 5c 9h 2s 7c 4h 6h Jh Kc 7s 4s 2h Td Ks 3h 8c Tc Qs 4c 5s 6s 9d 4d 3c 2d Ad As Ts 3d Qh Ah 3s 9s Kd 7d 7h 8d Jd Qd
check-setseq-64-xsl-rr-rr-64.out:  64bit: 0xb8185706068f20a8 0xfb60ad1fed610a2e 0xb62ccca53911c946 0x7079824fd94c9c1c 0xefe7a5fa0d4b401a 0xf0606ad392ee3d83
check-setseq-64-xsl-rr-rr-64.out:  Coins: TTTTHHHTTTHHTTTTHHTTHTTTHHTHTTHTTHTHHTHTTHHHTHTHHTHTTTTTHTHHHHHHT
check-setseq-64-xsl-rr-rr-64.out:  Rolls: 1 4 5 5 4 4 2 1 1 4 3 5 2 1 2 6 6 5 5 1 4 3 5 3 6 3 6 2 1 5 1 1 1
check-setseq-64-xsl-rr-rr-64.out:  Cards: 3s 9s 6c Qc Ac 5c Ts 2s 9h Qs 2h Ks 2c 5h Ah As 5d 7d 8h Js Jc 3c 5s 4s 7h 8c Th 4h Td 3h Tc 9c 6s Kd 8s 9d Jd Jh 4d 7s 6d Kc Qh 6h Ad Qd 7c 3d 8d Kh 4c 2d
check-setseq-8-rxs-m-xs-8.out:   8bit: 0xea 0x4d 0x8a 0x45 0x6b 0x23 0xcb 0xaa 0xf7 0x63 0xec 0x30 0x39 0xbc
check-setseq-8-rxs-m-xs-8.out:  Coins: TTTHHTTHHTTHTHHTTHTHTHTTTHTHHHTHHHHTHHHHHTTHHTHTTTTTTTTTHTHHHHHHH
check-setseq-8-rxs-m-xs-8.out:  Rolls: 5 2 3 6 5 2 5 3 5 4 1 5 3 2 2 4 6 3 5 6 3 4 1 3 4 2 4 1 2 5 3 4 6
check-setseq-8-rxs-m-xs-8.out:  Cards: 4c 9d Kd Td Tc 5s 3d 6c 9s Ts 6h 2h 2d 9h Qc Qh 2c 5c 8d 9c Kc Th Js Qs 6s 7c 8c As 6d Jd Qd 2s Jc 5d 4s 7s Jh 3c 3h 8s 7d Ks 8h 5h 3s Kh Ad Ah 4h Ac 7h 4d
"""
