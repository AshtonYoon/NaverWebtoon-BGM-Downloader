#include <stdio.h>
#pragma warning (disable:4996)

int main() {
	int n, k;
	printf("n입력 : ");
	scanf("%d", &n);

	printf("n입력 : ");
	scanf("%d", &k);

	for (int i = 1; i <= n; i++)
	{
		for (int j = n; j <= k; j++) {
			printf("%d, %d", i, j);
		}
	}
}