# Generate test data from source PCG C library

* Use Linux or [WSL](https://docs.microsoft.com/en-us/windows/wsl/install-win10)
* Get code from https://github.com/imneme/pcg-c
* Make main library
  * `make`
* Verify original tests succeed in `test-low`
  * `cd test-low`
  * `make test`
* Copy our custom files (`test-custom/*` or `test-max/*`) over the top of `test-low` files
  * Can optionally update seed values in `*.c` files, and/or number of generated values in `check-base.c`. If so, also update `TestDataCustomSeed.fs` or `TestDataMaxSeed.fs`.
* Clean and re-run
  * `make clean`
  * `make test`
* Should produce `actual/actual.txt` containing values to test against. Paste that data into the `let expected = ...` variable in `TestDataCustomSeed.fs` or `TestDataMaxSeed.fs`
