import socket

def main():
	json = r'{"jsonrpc": "2.0","method": "coder.press_key","params": {"key": "start"}}'
	json2 = r'{"jsonrpc": "2.0","method": "sum","params": {"a": 1, "b": 2}}'
	skt = socket.socket()
	skt.connect(('127.0.0.1', 6666))
	skt.send(bytearray(json, encoding='utf-8'))
	skt.close()
	

if __name__ == '__main__':
	main()
