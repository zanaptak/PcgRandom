#define XX_PREDECLS

#define XX_INFO \
            "pcg_oneseq_64_rxs_m_xs_64_random_r:\n" \
            "      -  result:      64-bit unsigned int (uint64_t)\n" \
            "      -  period:      2^64\n" \
            "      -  state type:  struct pcg_state_64 (%zu bytes)\n" \
            "      -  output func: RXS-M-XS\n" \
            "\n", sizeof(struct pcg_state_64)

#define XX_NUMBITS                  "  64bit:"
#define XX_NUMVALUES                6
#define XX_NUMWRAP                  3
#define XX_PRINT_RNGVAL(value)      printf(" 0x%016" PRIx64 "", value)
#define XX_RAND_DECL                struct pcg_state_64 rng;
#define XX_SEEDSDECL(seeds)         uint64_t seeds[1];
#define XX_SRANDOM_SEEDARGS(seeds)  seeds[0]
#define XX_SRANDOM_SEEDCONSTS       18446744073709551615ULL
#define XX_SRANDOM(...)             \
            pcg_oneseq_64_srandom_r(&rng, __VA_ARGS__)
#define XX_RANDOM()                 \
            pcg_oneseq_64_rxs_m_xs_64_random_r(&rng)
#define XX_BOUNDEDRAND(bound)       \
            pcg_oneseq_64_rxs_m_xs_64_boundedrand_r(&rng, bound)
#define XX_ADVANCE(delta)           \
            pcg_oneseq_64_advance_r(&rng, delta)

#include "pcg_variants.h"
#include "check-base.c"
