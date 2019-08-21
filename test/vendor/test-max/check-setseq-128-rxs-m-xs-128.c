#define XX_PREDECLS                                                            \
    void print128(pcg128_t value)                                              \
    {                                                                          \
        printf(" 0x%016" PRIx64 "%016" PRIx64 "", (uint64_t)(value >> 64), (uint64_t)value); \
    }

#define XX_INFO \
            "pcg_setseq_128_rxs_m_xs_128_random_r:\n" \
            "      -  result:      128-bit unsigned int (pcg128_t)\n" \
            "      -  period:      2^128   (* 2^127 streams)\n" \
            "      -  state type:  struct pcg_state_setseq_128 (%zu bytes)\n" \
            "      -  output func: RXS-M-XS\n" \
            "\n", sizeof(struct pcg_state_setseq_128)

#define XX_NUMBITS                  " 128bit:"
#define XX_NUMVALUES                6
#define XX_NUMWRAP                  2
#define XX_PRINT_RNGVAL(value)      print128( value)
#define XX_RAND_DECL                struct pcg_state_setseq_128 rng;
#define XX_SEEDSDECL(seeds)         pcg128_t seeds[2];
#define XX_SRANDOM_SEEDARGS(seeds)  seeds[0], seeds[1]
#define XX_SRANDOM_SEEDCONSTS       (PCG_128BIT_CONSTANT(18446744073709551615ULL,18446744073709551615ULL)), (PCG_128BIT_CONSTANT(18446744073709551615ULL,18446744073709551615ULL))
#define XX_SRANDOM(...)             \
            pcg_setseq_128_srandom_r(&rng, __VA_ARGS__)
#define XX_RANDOM()                 \
            pcg_setseq_128_rxs_m_xs_128_random_r(&rng)
#define XX_BOUNDEDRAND(bound)       \
            pcg_setseq_128_rxs_m_xs_128_boundedrand_r(&rng, bound)
#define XX_ADVANCE(delta)           \
            pcg_setseq_128_advance_r(&rng, delta)

#include "pcg_variants.h"
#if PCG_HAS_128BIT_OPS
    #include "check-base.c"
#else
    #include <stdio.h>
    int main()
    {
        printf("This platform does not support 128-bit integers.\n");
        return 1;
    }
#endif
